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
        Task<IEnumerable<Group>> GetAllGroupsByPersonIdAsync(int personId);
        Task<IEnumerable<Person>> GetAllPersonsByGroupIdAsync(int groupId);
        Task<GroupMemberResponse> AssignGroupMemberAsync(int groupId, int personId, bool personCreator);
        Task<GroupMemberResponse> UnassignGroupMemberAsync(int groupId, int personId);
    }
}
