using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.User;
using Ofernandoavila.Mailman.Business.Models.AccessControl;

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

    [HttpPost]
    public async Task<IActionResult> Add(UserInsertViewModel model)
    {
        if(!ModelState.IsValid) return CustomResponse(ModelState);

        if (!await _userService.Add(_mapper.Map<User>(model)))
            return CustomResponse();

        await Complete();
        return Ok();
    }

    private async Task<UserViewModel> GetUser(Guid id)
    {
        return _mapper.Map<UserViewModel>(await _userService.GetById(id));
    }

    private async Task Complete()
    {
        await _userService.Complete();
    }
}