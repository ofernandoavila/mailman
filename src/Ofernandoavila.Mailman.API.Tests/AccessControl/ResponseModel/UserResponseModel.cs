using Ofernandoavila.Mailman.Api.ViewModels.AccessControl;
using Ofernandoavila.Mailman.API.Tests._Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ofernandoavila.Mailman.API.Tests.AccessControl.ResponseModel
{
    public class UserResponseModel : BaseResponseModel
    {
        public UserViewModel Data { get; set; }
    }
}
