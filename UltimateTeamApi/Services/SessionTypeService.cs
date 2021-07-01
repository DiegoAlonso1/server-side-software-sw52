using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Services
{
    public class SessionTypeService : ISessionTypeService
    {
        private readonly ISessionTypeRepository _sessionTypeService;

        public SessionTypeService(ISessionTypeRepository sessionTypeService)
        {
            _sessionTypeService = sessionTypeService;
        }
        public async Task<IEnumerable<SessionType>> GetAllAsync()
        {
            return await _sessionTypeService.ListAsync();
        }

        public async Task<SessionTypeResponse> GetByIdAsync(int sessionTypeId)
        {
            var existingSessionType = await _sessionTypeService.FindByIdAsync(sessionTypeId);

            if (existingSessionType == null)
                return new SessionTypeResponse("SessionType not found");

            return new SessionTypeResponse(existingSessionType);
        }
    }
}
