using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<Group>> GetAllAsync();
        Task<GroupResponse> GetByIdAsync(int groupId);
        Task<GroupResponse> SaveAsync(Group group);
        Task<GroupResponse> UpdateAsync(int groupId, Group groupRequest);
        Task<GroupResponse> DeleteAsync(int groupId);
    }
}
