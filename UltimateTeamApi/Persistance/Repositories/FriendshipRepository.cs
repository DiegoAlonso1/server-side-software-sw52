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
    public class FriendshipRepository : BaseRepository, IFriendshipRepository
    {
        public FriendshipRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Friendship friendship)
        {
            await _context.Friendships.AddAsync(friendship);
        }

        public async Task AssignFriendAsync(int principalId, int friendId)
        {
            var friendship = await FindByPrincipalIdAndFriendIdAsync(principalId, friendId);

            if (friendship == null)
            {
                friendship = new Friendship { PrincipalId = principalId, FriendId = friendId};
                await AddAsync(friendship);
            }

            else
                throw new Exception("Both users have already been asigned.");
        }

        public async Task<Friendship> FindByPrincipalIdAndFriendIdAsync(int principalId, int friendId)
        {
            return await _context.Friendships
                .Where(f => f.PrincipalId == principalId && f.FriendId == friendId)
                .Include(f => f.Friend)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> ListFriendsByUserIdAsync(int userId)
        {
            var friendships = await _context.Friendships
                .Where(f => f.PrincipalId == userId || f.FriendId == userId)
                .Include(f => f.Friend)
                .Include(f => f.Principal)
                .ToListAsync();

            List<User> friends = new List<User>();

            foreach (var friendship in friendships)
            {
                if (friendship.FriendId != userId)
                    friends.Add(friendship.Friend);
                else
                    friends.Add(friendship.Principal);
            }

            return friends;
        }

        public void Remove(Friendship friendship)
        {
            _context.Friendships.Remove(friendship);
        }

        public async Task UnassignFriendAsync(int principalId, int friendId)
        {
            Friendship friendship = await FindByPrincipalIdAndFriendIdAsync(principalId, friendId);

            if (friendship != null)
                Remove(friendship);
            
            else
                throw new Exception("Both users have already been unasigned.");
        }
    }
}
