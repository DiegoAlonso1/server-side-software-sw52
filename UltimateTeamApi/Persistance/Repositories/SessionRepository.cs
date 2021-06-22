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
    public class SessionRepository : BaseRepository, ISessionRepository
    {
        public SessionRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Session session)
        {
            await _context.Sesssions.AddAsync(session);
        }

        public async Task<Session> FindBySessionIdAsync(int sessionId)
        {
            return await _context.Sesssions.FindAsync(sessionId);
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Sesssions.ToListAsync();
        }

        public void Update(Session session)
        {
            _context.Sesssions.Update(session);
        }
    }
}
