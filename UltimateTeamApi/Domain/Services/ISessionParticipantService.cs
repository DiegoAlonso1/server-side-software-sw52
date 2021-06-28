using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface ISessionParticipantService
    {
        Task<IEnumerable<SessionParticipant>> GetAllByPersonIdAsync(int personId);
        Task<IEnumerable<SessionParticipant>> GetAllBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionParticipant>> GetAllByPersonCreatorIdAsync(int personId);
        Task<SessionParticipantResponse> AssignSessionParticipantAsync(int sessionId, int personId, bool creator);
    }
}
