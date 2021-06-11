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
    public class FunctionalityRepository : BaseRepository, IFunctionalityRepository
    {
        public FunctionalityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Functionality> FindByIdAsync(int functionalityId)
        {
            return await _context.Functionalities.FindAsync(functionalityId);
        }

        public async Task<IEnumerable<Functionality>> ListAsync()
        {
            return await _context.Functionalities.ToListAsync();
        }
    }
}
