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

        public async Task AssignSessionParticipantAsync(int sessionId, int personId, bool creator)
        {
            var sessionParticipant = await FindBySessionIdAndPersonIdAsync(sessionId, personId);

            if (sessionParticipant == null)
            {
                sessionParticipant = new SessionParticipant { SessionId = sessionId, PersonId = personId, Creator = creator };
                await AddAsync(sessionParticipant);
            }

            else
                throw new Exception("Session and Person have already been asigned.");
        }

        public async Task<SessionParticipant> FindBySessionIdAndPersonIdAsync(int sessionId, int personId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.SessionId == sessionId && sp.PersonId == personId)
                .Include(sp => sp.Session)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SessionParticipant>> ListBySessionIdAsync(int sessionId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionParticipant>> ListByPersonCreatorIdAsync(int personId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.PersonId == personId && sp.Creator)
                .Include(sp => sp.Session)
                .ToListAsync();
        }

        public async Task<IEnumerable<SessionParticipant>> ListByPersonIdAsync(int personId)
        {
            return await _context.SessionParticipants
                .Where(sp => sp.PersonId == personId)
                .Include(sp => sp.Session)
                .ToListAsync();
        }
    }
}
