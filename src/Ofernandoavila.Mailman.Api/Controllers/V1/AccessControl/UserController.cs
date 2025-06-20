using Asp.Versioning;
using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.Api.ViewModels.DTO;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Services.AccessControl;
using Ofernandoavila.Mailman.Business.Interfaces.User;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using System.Linq.Expressions;

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

    [HttpGet()]
    [Authorize(Roles = "System, Developer")]
    public async Task<IActionResult> GetForGrid([FromQuery] UserFilter filter)
    {
        return await GetAll(filter);
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

    private async Task<IActionResult> GetAll(UserFilter filter)
    {
        var predicate = _mapper.Map <Expression<Func<User, bool>>>(GetFilterExpression(filter));
        var totalRecords = await _userService.GetTotal(predicate);
        filter.SetPaginationDefaults(totalRecords);

        var paged = new Paged<UserViewModel>
        {
            TotalRecords = totalRecords,
            PagedData = _mapper.Map<IEnumerable<UserViewModel>>(await _userService.GetAll(filter.PageNumber,
                                                                                                filter.PageSize,
                                                                                                predicate,
                                                                                                GetOrderByExpression(filter),
                                                                                                filter.Desc))
        };

        return CustomResponse(paged);
    }

    private static Expression<Func<User, bool>> GetFilterExpression(UserFilter filters)
    {
        var predicate = PredicateBuilder.New<User>(u => u.Active == true);

        if (!string.IsNullOrEmpty(filters.Search))
            predicate.And(u => u.Name.Contains(filters.Search) ||
                               u.Email.Contains(filters.Search));

        if (filters.RoleId != Guid.Empty)
            predicate.And(u => u.RoleId == filters.RoleId);

        if (filters.Active != null)
            predicate.And(u => u.Active == filters.Active);

        return predicate;
    }

    private static Expression<Func<User, object>> GetOrderByExpression(UserFilter filter)
    {
        filter.OrderBy ??= string.Empty;

        return filter.OrderBy.ToLower() switch
        {
            "active" => u => u.Active,
            "email" => u => u.Email,
            "role" => u => u.Role.Description,
            "name" or
            _ => u => u.Name,
        };
    }

    private async Task Complete()
    {
        await _userService.Complete();
    }
}