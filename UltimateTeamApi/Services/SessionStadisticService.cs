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
    public class SessionStadisticService : ISessionStadisticService
    {
        private readonly ISessionStadisticRepository _sessionStadisticRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionStadisticService(ISessionStadisticRepository sessionStadisticRepository, IUnitOfWork unitOfWork)
        {
            _sessionStadisticRepository = sessionStadisticRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionStadisticResponse> AssignSessionStadisticAsync(int sessionId, int functionalityId)
        {
            try
            {
                await _sessionStadisticRepository.AssignSessionStadisticAsync(sessionId, functionalityId);
                await _unitOfWork.CompleteAsync();
                var sessionStadistic = await _sessionStadisticRepository.FindBySessionIdAndFunctionalityIdAsync(sessionId, functionalityId);
                return new SessionStadisticResponse(sessionStadistic);
            }
            catch (Exception ex)
            {
                return new SessionStadisticResponse($"An error ocurred while assigning a SessionStadistic: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SessionStadistic>> GetAllByFunctionalityIdAsync(int functionalityId)
        {
            return await _sessionStadisticRepository.ListByFunctionalityIdAsync(functionalityId);
        }

        public async Task<IEnumerable<SessionStadistic>> GetAllBySessionIdAsync(int sessionId)
        {
            return await _sessionStadisticRepository.ListBySessionIdAsync(sessionId);
        }
    }
}
