using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class SessionTypeResponse : BaseResponse<SessionType>
    {
        public SessionTypeResponse(SessionType resource) : base(resource)
        {
        }

        public SessionTypeResponse(string message) : base(message)
        {
        }
    }
}