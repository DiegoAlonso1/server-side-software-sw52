using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Services.Communications
{
    public class SessionParticipantResponse : BaseResponse<SessionParticipant>
    {
        public SessionParticipantResponse(SessionParticipant resource) : base(resource)
        {
        }

        public SessionParticipantResponse(string message) : base(message)
        {
        }
    }
}