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
    public class GroupMemberRepository : BaseRepository, IGroupMemberRepository
    {
        public GroupMemberRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(GroupMember groupMember)
        {
            await _context.GroupMembers.AddAsync(groupMember);
        }

        public async Task AssignGroupMemberAsync(int groupId, int userId)
        {
            GroupMember groupMember = await FindByGroupIdAndUserIdAsync(groupId, userId);
            if (groupMember == null)
            {
                groupMember = new GroupMember { GroupId = groupId, UserId = userId };
                await AddAsync(groupMember);
            }
        }

        public async Task<GroupMember> FindByGroupIdAndUserIdAsync(int groupId, int userId)
        {
            return await _context.GroupMembers.FindAsync(groupId,userId);
        }

        public async Task<IEnumerable<User>> ListUsersByGroupIdAsync(int groupId)
        {
            var usersId = await _context.GroupMembers
                .Where(pt => pt.GroupId == groupId)
                .Select(pt => pt.UserId)
                .ToListAsync();

            List<User> users = new List<User>();
            foreach(int userId in usersId)
            {
                var user = await _context.Users.FindAsync(userId);
                users.Add(user);
            }
            return users;
        }

        public async Task<IEnumerable<Group>> ListGroupsByUserIdAsync(int userId)
        {
            var groupsId = await _context.GroupMembers
                .Where(pt => pt.UserId == userId)
                .Select(pt => pt.GroupId)
                .ToListAsync();

            List<Group> groups = new List<Group>();
            foreach (int groupId in groupsId)
            {
                var group = await _context.Groups.FindAsync(groupId);
                groups.Add(group);
            }
            return groups;
        }

        public void Remove(GroupMember groupMember)
        {
            _context.GroupMembers.Remove(groupMember);
        }

        public async Task UnassignGroupMemberAsync(int groupId, int userId)
        {
            GroupMember groupMember = await FindByGroupIdAndUserIdAsync(groupId, userId);
            if (groupMember != null)
                Remove(groupMember);
        }
    }
}
