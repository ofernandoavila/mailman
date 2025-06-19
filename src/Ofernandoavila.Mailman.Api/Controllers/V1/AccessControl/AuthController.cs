using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.User;
using Ofernandoavila.Mailman.Business.Models.Settings;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;

namespace Ofernandoavila.Mailman.Api.Controllers.V1.AccessControl;

[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiversion}")]
public class AuthController(IUserService userService,
                            ISessionService sessionService,
                            IMapper mapper,
                            IOptions<AppSettings> appSettings,
                            INotificator notificator,
                            IUser appUser) : MainController(notificator, appUser)
{
    private readonly IUserService _userService = userService;
    private readonly ISessionService _sessionService = sessionService;
    private readonly IMapper _mapper = mapper;
    private readonly AppSettings _appSettings = appSettings.Value;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginViewModel userLogin)
    {
        if(!ModelState.IsValid) return CustomResponse(ModelState);
        string refreshToken = Guid.NewGuid().ToString();

        var user = await _userService.GetByEmailAndPassword(userLogin.Email, userLogin.Password);

        if(user is null)
        {
            NotifyError("Wrong email and/or password.");
            return CustomResponse(userLogin);
        }

        UserLoginResponseViewModel userLoginResponseViewModel = await GenerateJWT(user, refreshToken);
        await StartSession(userLoginResponseViewModel, refreshToken);

        return CustomResponse(userLoginResponseViewModel);
    }

    private async Task<UserLoginResponseViewModel> GenerateJWT(User user, string refreshToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenExpirationMinutes);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emitter,
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Role, user.Role.Description),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("FirstAccess", user.FirstAccess.ToString()),
            }),
            Claims = new Dictionary<string, object>
            {
                { JwtRegisteredClaimNames.Aud, _appSettings.ValidIn }
            },
            Expires = expires,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
                                                            SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        var response = new UserLoginResponseViewModel
        {
            AccessToken = encodedToken,
            RefreshToken = refreshToken,
            ExpiresIn = TimeSpan.FromTicks(expires.Ticks).TotalSeconds,
            UserToken = new UserTokenViewModel
            {
                Id = user.Id,
                RoleId = user.Role.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.Role.Description,
                RefreshToken = refreshToken,
                FirstAccess = user.FirstAccess,
                Active = user.Active,
            }
        };

        return await Task.FromResult(response);
    }

    private async Task StartSession(UserLoginResponseViewModel userLoginResponseViewModel, string refreshToken)
    {
        var sessionViewModel = new SessionViewModel
        {
            DateTime = DateTime.Now,
            ExpirationDateTime = DateTime.UtcNow.AddMinutes(_appSettings.TokenExpirationMinutes),
            UserAgent = _appUser.GetUserAgent(),
            Token = userLoginResponseViewModel.AccessToken,
            RefreshToken = refreshToken,
            UserId = userLoginResponseViewModel.UserToken.Id
        };

        await _sessionService.Add(_mapper.Map<Session>(sessionViewModel));
        await Complete();
    }

    private async Task Complete()
    {
        await _sessionService.Complete();
        await _userService.Complete();
    }
}