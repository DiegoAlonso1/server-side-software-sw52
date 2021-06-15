using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IAdministratorRepository
    {
        Task<IEnumerable<Administrator>> ListAsync();
        Task<IEnumerable<Administrator>> ListByAreaAsync(string area);
        Task<Administrator> FindByIdAsync(int administratorId);
        Task AddAsync(Administrator administrator);
        void Update(Administrator administrator);
        void Remove(Administrator administrator);
    }
}
