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
        Task<IEnumerable<SessionParticipant>> GetAllAsync();
        Task<SessionParticipantResponse> GetByIdAsync(int sessionParticipantId);
        Task<SessionParticipantResponse> UpdateAsync(int userId, User userRequest);
        Task<SessionParticipantResponse> DeleteAsync(int userId);
    }
}
