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
    public class SessionParticipantService : ISessionParticipantService
    {
        private readonly ISessionParticipantRepository _sessionParticipantRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionParticipantService(ISessionParticipantRepository sessionParticipantRepository, IUnitOfWork unitOfWork)
        {
            _sessionParticipantRepository = sessionParticipantRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SessionParticipantResponse> AssignSessionParticipantAsync(int sessionId, int userId, bool creator)
        {
            try
            {
                await _sessionParticipantRepository.AssignSessionParticipantAsync(sessionId, userId, creator);
                await _unitOfWork.CompleteAsync();
                var sessionParticipant = await _sessionParticipantRepository.FindBySessionIdAndUserIdAsync(sessionId, userId);
                return new SessionParticipantResponse(sessionParticipant);
            }
            catch (Exception ex)
            {
                return new SessionParticipantResponse($"An error ocurred while assigning a SessionParticipant: {ex.Message}");
            }
        }

        public async Task<IEnumerable<SessionParticipant>> GetAllBySessionIdAsync(int sessionId)
        {
            return await _sessionParticipantRepository.ListBySessionIdAsync(sessionId);
        }

        public async Task<IEnumerable<SessionParticipant>> GetAllByUserCreatorIdAsync(int userId)
        {
            return await _sessionParticipantRepository.ListByUserCreatorIdAsync(userId);
        }

        public async Task<IEnumerable<SessionParticipant>> GetAllByUserIdAsync(int userId)
        {
            return await _sessionParticipantRepository.ListByUserIdAsync(userId);
        }
    }
}
