using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> ListAsync();
        Task<IEnumerable<Person>> ListByAdministratorIdAsync(int administratorId);
        Task<Person> FindByIdAsync(int personId);
        Task<Person> FindByEmailAsync(string email);
        Task AddAsync(Person person);
        void Update(Person person);
        void Remove(Person person);
    }
}
