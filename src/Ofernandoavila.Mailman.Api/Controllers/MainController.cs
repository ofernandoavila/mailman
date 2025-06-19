using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ofernandoavila.Mailman.Api.Extensions;
using Ofernandoavila.Mailman.Business.Interfaces.Notification;
using Ofernandoavila.Mailman.Business.Interfaces.User;

namespace Ofernandoavila.Mailman.Api.Controllers;

[ApiController]
[AllowAsynchronousIO]
public abstract class MainController : ControllerBase
{
    public readonly IUser _appUser;
    private readonly INotificator _notificator;
    protected Guid UserId { get; set; }
    protected string UserRole { get; set; }
    protected bool FirstAccess { get; set; }
    protected bool IsUserAuthenticated { get; set; }
    public string UserEmail { get; set; }
    public string UserToken { get; set; }

    public MainController(INotificator notificator, IUser appUser)
    {
        _notificator = notificator;

        if(_appUser.IsAuthenticated())
        {
            UserId = _appUser.GetUserId();
            UserRole = _appUser.GetUserRole();
            UserEmail = _appUser.GetUserEmail();
            UserToken = _appUser.GetUserToken().Result;
            IsUserAuthenticated = true;
            FirstAccess = _appUser.GetFirstAccess();
        }
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if(!modelState.IsValid)
            NotificateInvalidModelError(modelState);

        return CustomResponse();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if(!_notificator.HasNotification())
        {
            return Ok(
                new
                {
                    success = true,
                    data = result
                }
            );
        }

        return BadRequest(
            new
            {
                success = false,
                errors = _notificator.GetNotifications().Select( n => n.Message)
            }
        );
    }

    protected void NotificateInvalidModelError(ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany( e => e.Errors );

        foreach(var error in errors)
        {
            var errorMessage = error.Exception is null ? error.ErrorMessage : error.Exception.Message;
            NotifyError(errorMessage);
        }
    }

    protected void NotifyError(string errorMessage)
    {
        _notificator.Handle(new Business.Models.Settings.Notification(errorMessage));
    }

    protected bool CanUserUpdateProfile(Guid id)
    {
        return IsSameUser(id) || IsAdminUser();
    }

    private bool IsSameUser(Guid id)
    {
        return UserId == id;
    }

    private bool IsAdminUser()
    {
        return _appUser.IsInRole("Administrator") || _appUser.IsInRole("System");
    }
}