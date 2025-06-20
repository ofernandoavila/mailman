using Asp.Versioning;
using AutoMapper;
using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.Api.ViewModels.DTO;
using Ofernandoavila.Mailman.Api.ViewModels.License;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.Services.License;
using Ofernandoavila.Mailman.Business.Interfaces.User;
using System.Linq.Expressions;

namespace Ofernandoavila.Mailman.Api.Controllers.V1.License;

[ApiVersion("1.0")]
[Authorize]
[Route("api/v{version:apiversion}/[controller]")]
public class LicenseController(IMapper mapper, ILicenseService licenseService, INotificator notificator, IUser appUser) : MainController(notificator, appUser)
{
    private readonly IMapper _mapper = mapper;
    private readonly ILicenseService _licenseService = licenseService;

    [HttpPost("new")]
    public async Task<IActionResult> GenerateNewLicense(LicenseInsertViewModel license)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        Business.Models.License.License model = _mapper.Map<Business.Models.License.License>(license);

        model.SetUserId(_appUser.GetUserId());

        if (!await _licenseService.Add(model))
            return CustomResponse();

        await Complete();

        return Created();
    }

    [HttpGet()]
    [Authorize(Roles = "System, Developer")]
    public async Task<IActionResult> GetForGrid([FromQuery] LicenseFilter filter)
    {
        return await GetAll(filter);
    }

    private async Task<IActionResult> GetAll(LicenseFilter filter)
    {
        var predicate = _mapper.Map<Expression<Func<Business.Models.License.License, bool>>>(GetFilterExpression(filter));
        var totalRecords = await _licenseService.GetTotal(predicate);
        filter.SetPaginationDefaults(totalRecords);

        var paged = new Paged<LicenseViewModel>
        {
            TotalRecords = totalRecords,
            PagedData = _mapper.Map<IEnumerable<LicenseViewModel>>(await _licenseService.GetAll(filter.PageNumber,
                                                                                                filter.PageSize,
                                                                                                predicate,
                                                                                                GetOrderByExpression(filter),
                                                                                                filter.Desc))
        };

        return CustomResponse(paged);
    }

    private static Expression<Func<Business.Models.License.License, bool>> GetFilterExpression(LicenseFilter filters)
    {
        var predicate = PredicateBuilder.New<Business.Models.License.License>(u => u.Active == true);

        if (!string.IsNullOrEmpty(filters.Search))
            predicate.And(u => u.ApplicationName.Contains(filters.Search) ||
                               u.Hosts.Contains(filters.Search));

        if (filters.Active != null)
            predicate.And(u => u.Active == filters.Active);

        return predicate;
    }

    private static Expression<Func<Business.Models.License.License, object>> GetOrderByExpression(LicenseFilter filter)
    {
        filter.OrderBy ??= string.Empty;

        return filter.OrderBy.ToLower() switch
        {
            "active" => u => u.Active,
            "applicationName" or
            _ => u => u.ApplicationName,
        };
    }

    private async Task Complete()
    {
        await _licenseService.Complete();
    }
}