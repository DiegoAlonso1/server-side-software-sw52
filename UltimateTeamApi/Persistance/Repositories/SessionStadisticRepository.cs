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
    public class SessionStadisticRepository : BaseRepository, ISessionStadisticRepository
    {
        public SessionStadisticRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(SessionStadistic sessionStadistic)
        {
            await _context.SessionStadistics.AddAsync(sessionStadistic);
        }

        public async Task AssignSessionStadisticAsync(int sessionId, int functionalityId)
        {
            var sessionStadistic = await FindBySessionIdAndFunctionalityIdAsync(sessionId, functionalityId);

            if (sessionStadistic == null)
            {
                sessionStadistic = new SessionStadistic { SessionId = sessionId, FunctionalityId = functionalityId, Count = 0 };
                await AddAsync(sessionStadistic);
            }

            else
                throw new Exception("Session and functionality have already been asigned.");
        }

        public async Task<SessionStadistic> FindBySessionIdAndFunctionalityIdAsync(int sessionId, int functionalityId)
        {
            return await _context.SessionStadistics
                .Where(s => s.SessionId == sessionId && s.FunctionalityId == functionalityId)
                .Include(s => s.Session)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SessionStadistic>> ListByFunctionalityIdAsync(int functionalityId)
        {
            return await _context.SessionStadistics
                .Where(s => s.FunctionalityId == functionalityId)
                .Include(s => s.Session)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionStadistic>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.SessionStadistics
                .Where(s => s.SessionId == sessionId)
                .Include(s => s.Functionality)
                .ToListAsync();
        }
    }
}
