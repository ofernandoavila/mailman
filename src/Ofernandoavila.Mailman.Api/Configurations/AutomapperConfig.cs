using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.Api.ViewModels.License;
using Ofernandoavila.Mailman.Api.ViewModels.Parameter;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using Ofernandoavila.Mailman.Business.Models.License;
using Ofernandoavila.Mailman.Business.Models.Parameter;

namespace Ofernandoavila.Mailman.Api.Configurations;

[ExcludeFromCodeCoverage]
public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Session, SessionViewModel>().ReverseMap();
        CreateMap<User, UserCreateViewModel>().ReverseMap();
        CreateMap<User, UserLoginViewModel>().ReverseMap();
        CreateMap<User, UserInsertViewModel>().ReverseMap();
        CreateMap<User, UserViewModel>().ReverseMap();

        CreateMap<License, LicenseViewModel>().ReverseMap();
        CreateMap<License, LicenseInsertViewModel>().ReverseMap();

        CreateMap<Role, RoleViewModel>().ReverseMap();
    }
}