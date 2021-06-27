using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISessionParticipantRepository
    {
        Task<IEnumerable<SessionParticipant>> ListByUserIdAsync(int userId);
        Task<IEnumerable<SessionParticipant>> ListBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionParticipant>> ListByUserCreatorIdAsync(int userId);
        Task<SessionParticipant> FindBySessionIdAndUserIdAsync(int sessionId, int userId);
        Task AddAsync(SessionParticipant sessionParticipant);
        Task AssignSessionParticipantAsync(int sessionId, int userId, bool creator);
    }
}
