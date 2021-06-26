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
    public class AdministratorRepository : BaseRepository, IAdministratorRepository
    {
        public AdministratorRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Administrator administrator)
        {
            await _context.AddAsync(administrator);
        }

        public async Task<Administrator> FindByIdAsync(int administratorId)
        {
            return await _context.Administrators.FindAsync(administratorId);
        }

        public async Task<IEnumerable<Administrator>> ListAsync()
        {
            return await _context.Administrators.ToListAsync();
        }

        public async Task<IEnumerable<Administrator>> ListByAreaAsync(string area)
        {
            return await _context.Administrators.Where(a => a.Area == area).ToListAsync();
        }

        public void Remove(Administrator administrator)
        {
            _context.Administrators.Remove(administrator);
        }

        public void Update(Administrator administrator)
        {
            _context.Administrators.Update(administrator);
        }
    }
}
