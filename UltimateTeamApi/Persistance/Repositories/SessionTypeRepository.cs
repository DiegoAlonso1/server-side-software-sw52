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
    public class SessionTypeRepository : BaseRepository, ISessionTypeRepository
    {
        public SessionTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<SessionType> FindByIdAsync(int sessionTypeId)
        {
            return await _context.SessionTypes.FindAsync(sessionTypeId);
        }

        public async Task<IEnumerable<SessionType>> ListAsync()
        {
            return await _context.SessionTypes.ToListAsync();
        }
    }
}