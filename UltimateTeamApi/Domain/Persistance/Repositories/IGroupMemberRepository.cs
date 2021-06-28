using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IGroupMemberRepository
    {
		Task<IEnumerable<Group>> ListGroupsByPersonIdAsync(int personId);
		Task<IEnumerable<Person>> ListPersonsByGroupIdAsync(int groupId);
		Task<GroupMember> FindByGroupIdAndPersonIdAsync(int groupId, int personId);
		Task AddAsync(GroupMember groupMember);
		void Remove(GroupMember groupMember);
		Task AssignGroupMemberAsync(int groupId, int personId, bool personCreator);
		Task UnassignGroupMemberAsync(int groupId, int personId);
	}
}
