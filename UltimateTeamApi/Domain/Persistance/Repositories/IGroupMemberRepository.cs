using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IGroupMemberRepository
    {
		Task<IEnumerable<Group>> ListGroupsByUserIdAsync(int userId);
		Task<IEnumerable<User>> ListUsersByGroupIdAsync(int groupId);
		Task<GroupMember> FindByGroupIdAndUserIdAsync(int groupId, int userId);
		Task AddAsync(GroupMember groupMember);
		void Remove(GroupMember groupMember);
		Task AssignGroupMemberAsync(int groupId, int userId);
		Task UnassignGroupMemberAsync(int groupId, int userId);
	}
}
