using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface ISessionStadisticService
    {
        Task<IEnumerable<SessionStadistic>> GetAllStadisticsBySessionIdAsync(int sessionId);
        Task<IEnumerable<SessionStadistic>> GetAllSessionsByFunctionalityIdAsync(int functionalityId);
        Task<SessionStadisticResponse> AssignSessionStadisticAsync(int sessionId, int functionalityId);
    }
}
