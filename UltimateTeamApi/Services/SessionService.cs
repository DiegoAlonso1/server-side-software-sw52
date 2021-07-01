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
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(ISessionRepository sessionRepository, IUnitOfWork unitOfWork)
        {
            _sessionRepository = sessionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await _sessionRepository.ListAsync();
        }

        public async Task<IEnumerable<Session>> GetAllByNameAsync(string sessionName)
        {
            return await _sessionRepository.ListByNameAsync(sessionName);
        }

        public async Task<SessionResponse> GetByIdAsync(int sessionId)
        {
            var existingSession = await _sessionRepository.FindByIdAsync(sessionId);

            if (existingSession == null)
                return new SessionResponse("Session not found");

            return new SessionResponse(existingSession);
        }

        public async Task<SessionResponse> SaveAsync(Session session)
        {
            try
            {
                await _sessionRepository.AddAsync(session);
                await _unitOfWork.CompleteAsync();

                return new SessionResponse(session);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while saving the session: {ex.Message}");
            }
        }

        public async Task<SessionResponse> UpdateAsync(int sessionId, Session sessionRequest)
        {
            var existingSession = await _sessionRepository.FindByIdAsync(sessionId);

            if (existingSession == null)
                return new SessionResponse("Session not found");

            existingSession.Name = sessionRequest.Name;
            existingSession.SessionTypeId = sessionRequest.SessionTypeId;

            try
            {
                _sessionRepository.Update(existingSession);
                await _unitOfWork.CompleteAsync();

                return new SessionResponse(existingSession);
            }
            catch (Exception ex)
            {
                return new SessionResponse($"An error ocurred while updating the session: {ex.Message}");
            }
        }
    }
}
