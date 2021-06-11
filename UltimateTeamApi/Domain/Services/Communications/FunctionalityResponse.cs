using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class FunctionalityResponse : BaseResponse<Functionality>
    {
        public FunctionalityResponse(Functionality resource) : base(resource)
        {
        }

        public FunctionalityResponse(string message) : base(message)
        {
        }
    }
}
