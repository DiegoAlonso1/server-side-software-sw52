using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface ISessionRepository
    {
        Task<IEnumerable<Session>> ListAsync();
        Task<IEnumerable<Session>> ListByNameAsync(string sessionName);
        Task<Session> FindByIdAsync(int sessionId);
        Task AddAsync(Session session);
        void Update(Session session);
    }
}