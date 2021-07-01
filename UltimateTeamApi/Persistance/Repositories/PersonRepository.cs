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
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
        }

        public async Task<Person> FindByIdAsync(int personId)
        {
            return await _context.Persons.FindAsync(personId);
        }

        public async Task<Person> FindByEmailAsync(string personEmail)
        {
            return await _context.Persons
                .Where(u => u.Email == personEmail)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> ListAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<IEnumerable<Person>> ListByAdministratorIdAsync(int administratorId)
        {
            return await _context.Persons.Where(u => u.AdministratorId == administratorId).ToListAsync();
        }

        public void Remove(Person person)
        {
            _context.Persons.Remove(person);
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
        }
    }
}
