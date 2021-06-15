using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class AdministratorResponse : BaseResponse<Administrator>
    {
        public AdministratorResponse(Administrator resource) : base(resource)
        {
        }

        public AdministratorResponse(string message) : base(message)
        {
        }
    }
}
