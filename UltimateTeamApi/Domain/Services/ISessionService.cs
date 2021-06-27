using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface ISessionService
    {
        Task<IEnumerable<Session>> GetAllAsync();
        Task<IEnumerable<Session>> GetAllByNameAsync(string sessionName);
        Task<SessionResponse> GetByIdAsync(int sessionId);
        Task<SessionResponse> SaveAsync(Session session);
        Task<SessionResponse> UpdateAsync(int sessionId, Session sessionRequest);
    }
}
