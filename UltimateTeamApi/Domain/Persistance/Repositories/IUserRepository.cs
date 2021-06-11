using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ListAsync();
        Task<IEnumerable<User>> ListByAdministratorIdAsync(int administratorId);
        Task<User> FindByIdAsync(int userId);
        Task AddAsync(User user);
        void Update(User user);
        void Remove(User user);
    }
}
