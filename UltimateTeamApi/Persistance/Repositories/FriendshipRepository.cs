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

        public async Task AssignFriendAsync(int user1Id, int user2Id)
        {
            var friendship = await FindByUser1IdAndUser2IdAsync(user1Id, user2Id);

            if (friendship == null)
            {
                friendship = new Friendship { User1Id = user1Id, User2Id = user2Id};
                await AddAsync(friendship);
            }

            else
                throw new Exception("User 1 and User 2 have already been asigned.");
        }

        public async Task<Friendship> FindByUser1IdAndUser2IdAsync(int user1Id, int user2Id)
        {
            return await _context.Friendships
                .Where(f => f.User1Id == user1Id && f.User2Id == user2Id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Friendship>> ListByUserIdAsync(int userId)
        {
            return await _context.Friendships
                .Where(f => f.User1Id == userId)
                .ToListAsync();
        }

        public void Remove(Friendship friendship)
        {
            _context.Friendships.Remove(friendship);
        }

        public async Task UnassignFriendAsync(int user1Id, int user2Id)
        {
            Friendship friendship = await FindByUser1IdAndUser2IdAsync(user1Id, user2Id);

            if (friendship != null)
                Remove(friendship);
            
            else
                throw new Exception("User 1 and User 2 have already been unasigned.");
        }
    }
}
