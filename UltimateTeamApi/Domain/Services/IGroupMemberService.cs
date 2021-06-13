using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IGroupMemberService
    {
        Task<IEnumerable<Group>> GetAllGroupsByUserIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersByGroupIdAsync(int groupId);
        Task<GroupMemberResponse> AssignGroupMemberAsync(int groupId, int userId);
        Task<GroupMemberResponse> UnassignGroupMemberAsync(int groupId, int userId);
    }
}
