using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Contexts;
using UltimateTeamApi.Domain.Persistance.Repositories;

namespace UltimateTeamApi.Persistance.Repositories
{
    public class SessionParticipantRepository : BaseRepository, ISessionParticipantRepository
    {
        public SessionParticipantRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SessionParticipant sessionParticipant)
        {
            await _context.SessionParticipants.AddAsync(sessionParticipant);
        }

        public async Task AssignSessionParticipantAsync(int sessionId, int userId, bool creator)
        {
            var sessionParticipant = await FindBySessionIdAndUserIdAsync(sessionId, userId);

            if (sessionParticipant == null)
            {
                sessionParticipant = new SessionParticipant { SessionId = sessionId, UserId = userId, Creator = creator };
                await AddAsync(sessionParticipant);
            }

            else
                throw new Exception("Session and User have already been asigned.");
        }

        public async Task<SessionParticipant> FindBySessionIdAndUserIdAsync(int sessionId, int userId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.SessionId == sessionId && sp.UserId == userId)
                .Include(sp => sp.Session)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SessionParticipant>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionParticipant>> ListByUserCreatorIdAsync(int userId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.UserId == userId && sp.Creator)
                .Include(sp => sp.Session)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionParticipant>> ListByUserIdAsync(int userId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.UserId == userId)
                .Include(sp => sp.Session)
                .ToListAsync();
        }
    }
}
