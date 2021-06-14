using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISessionRepostory
    {
        Task<IEnumerable<SessionParticipant>> ListByUserIdAsync(int userId);
        Task<IEnumerable<SessionParticipant>> ListBySessionAsync(int sessionId);
        Task<SessionParticipant> FindSessionIdandUserAsync(int sessionId, int userId);
        Task AddAsync(SessionParticipant sessionParticipant);
        Task AssignSessionParticipantAsync(int sessionId, int userId);
    }
}