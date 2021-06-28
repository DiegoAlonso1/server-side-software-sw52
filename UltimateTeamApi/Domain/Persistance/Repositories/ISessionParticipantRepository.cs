using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISessionParticipantRepository
    {
        Task<IEnumerable<SessionParticipant>> ListByPersonIdAsync(int personId);
        Task<IEnumerable<SessionParticipant>> ListBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionParticipant>> ListByPersonCreatorIdAsync(int personId);
        Task<SessionParticipant> FindBySessionIdAndPersonIdAsync(int sessionId, int personId);
        Task AddAsync(SessionParticipant sessionParticipant);
        Task AssignSessionParticipantAsync(int sessionId, int personId, bool creator);
    }
}
