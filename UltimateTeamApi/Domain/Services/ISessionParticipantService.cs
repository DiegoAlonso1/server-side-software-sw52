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
        Task<IEnumerable<SessionParticipant>> GetAllByUserIdAsync(int userId);
        Task<IEnumerable<SessionParticipant>> GetAllBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionParticipant>> GetAllByUserCreatorIdAsync(int userId);
        Task<SessionParticipantResponse> AssignSessionParticipantAsync(int sessionId, int userId, bool creator);
    }
}
