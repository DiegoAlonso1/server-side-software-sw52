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
        Task<SessionParticipantResponse> GetByIdAsync(int sessionId);
        Task<SessionParticipantResponse> UpdateAsync(int userId, User userRequest);
        Task<SessionParticipantResponse> DeleteAsync(int userId);
    }
}
