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
            await _context.Sessions.AddAsync(session);
        }

        public async Task<Session> FindByIdAsync(int sessionId)
        {
            return await _context.Sessions.FindAsync(sessionId);
        }

        public async Task<IEnumerable<Session>> ListAsync()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<IEnumerable<Session>> ListByNameAsync(string sessionName)
        {
            return await _context.Sessions.Where(s => s.Name == sessionName).ToListAsync();
        }

        public void Update(Session session)
        {
            _context.Sessions.Update(session);
        }
    }
}
