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

        public async Task<IEnumerable<GroupMember>> ListByGroupIdAsync(int groupId)
        {
            return await _context.GroupMembers
                .Where(pt => pt.GroupId == groupId)
                .Include(pt => pt.Group)
                .Include(pt => pt.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<GroupMember>> ListByUserIdAsync(int userId)
        {
            return await _context.GroupMembers
                .Where(pt => pt.UserId == userId)
                .Include(pt => pt.Group)
                .Include(pt => pt.User)
                .ToListAsync();
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
