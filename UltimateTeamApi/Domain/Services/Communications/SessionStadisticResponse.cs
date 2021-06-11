using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class SessionStadisticResponse : BaseResponse<SessionStadistic>
    {
        public SessionStadisticResponse(SessionStadistic resource) : base(resource)
        {
        }

        public SessionStadisticResponse(string message) : base(message)
        {
        }
    }
}
