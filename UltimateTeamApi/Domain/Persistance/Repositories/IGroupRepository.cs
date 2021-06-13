using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> ListAsync();
        Task<Group> FindByIdAsync(int groupId);
        Task AddAsync(Group group);
        void Update(Group group);
        void Remove(Group group);
    }
}
