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

        public async Task<SessionParticipantResponse> AssignSessionParticipantAsync(int sessionId, int personId, bool creator)
        {
            try
            {
                await _sessionParticipantRepository.AssignSessionParticipantAsync(sessionId, personId, creator);
                await _unitOfWork.CompleteAsync();
                var sessionParticipant = await _sessionParticipantRepository.FindBySessionIdAndPersonIdAsync(sessionId, personId);
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

        public async Task<IEnumerable<SessionParticipant>> GetAllByPersonCreatorIdAsync(int personId)
        {
            return await _sessionParticipantRepository.ListByPersonCreatorIdAsync(personId);
        }

        public async Task<IEnumerable<SessionParticipant>> GetAllByPersonIdAsync(int personId)
        {
            return await _sessionParticipantRepository.ListByPersonIdAsync(personId);
        }
    }
}
