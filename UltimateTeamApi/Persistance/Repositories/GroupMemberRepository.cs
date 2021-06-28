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

        public async Task AssignGroupMemberAsync(int groupId, int personId, bool personCreator)
        {
            GroupMember groupMember = await FindByGroupIdAndPersonIdAsync(groupId, personId);
            if (groupMember == null)
            {
                groupMember = new GroupMember { GroupId = groupId, PersonId = personId, PersonCreator = personCreator };
                await AddAsync(groupMember);
            }
        }

        public async Task<GroupMember> FindByGroupIdAndPersonIdAsync(int groupId, int personId)
        {
            return await _context.GroupMembers
                .Where(gm => gm.GroupId == groupId && gm.PersonId == personId)
                .Include(gm => gm.Group)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Person>> ListPersonsByGroupIdAsync(int groupId)
        {
            var personsId = await _context.GroupMembers
                .Where(pt => pt.GroupId == groupId)
                .Select(pt => pt.PersonId)
                .ToListAsync();

            List<Person> persons = new List<Person>();
            foreach(int personId in personsId)
            {
                var person = await _context.Persons.FindAsync(personId);
                persons.Add(person);
            }
            return persons;
        }

        public async Task<IEnumerable<Group>> ListGroupsByPersonIdAsync(int personId)
        {
            var groupsId = await _context.GroupMembers
                .Where(pt => pt.PersonId == personId)
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

        public async Task UnassignGroupMemberAsync(int groupId, int personId)
        {
            GroupMember groupMember = await FindByGroupIdAndPersonIdAsync(groupId, personId);
            if (groupMember != null)
                Remove(groupMember);
        }
    }
}
