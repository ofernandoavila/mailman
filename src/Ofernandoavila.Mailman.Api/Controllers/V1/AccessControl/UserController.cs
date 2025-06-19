using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.User;

namespace Ofernandoavila.Mailman.Api.Controllers.V1.AccessControl;

[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiversion}/[controller]")]
public class UserController(IMapper mapper, IUserService userService, INotificator notificator, IUser appUser) : MainController(notificator, appUser)
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserService _userService = userService;

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        if(CanUserUpdateProfile(id))
            return Forbid();

        var userViewModel = await GetUser(id);

        return CustomResponse(userViewModel);
    }

    private async Task<UserViewModel> GetUser(Guid id)
    {
        return _mapper.Map<UserViewModel>(await _userService.GetById(id));
    }
}