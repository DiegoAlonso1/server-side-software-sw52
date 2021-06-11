using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISessionStadisticRepository
    {
        Task<IEnumerable<SessionStadistic>> ListBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionStadistic>> ListByFunctionalityIdAsync(int functionalityId);
        Task<SessionStadistic> FindBySessionIdAndFunctionalityIdAsync(int sessionId, int functionalityId);
        Task AddAsync(SessionStadistic sessionStadistic);
        Task AssignSessionStadisticAsync(int sessionId, int functionalityId);
    }
}
