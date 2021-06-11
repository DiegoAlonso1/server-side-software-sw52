using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    interface IFriendshipRepository
    {
        Task<IEnumerable<Friendship>> ListByUserIdAsync(int userId);
        Task<Friendship> FindByUser1IdAndUser2IdAsync(int user1Id, int user2Id);
        Task AddAsync(Friendship friendship);
        void Remove(Friendship friendship);
        Task AssignFriendAsync(int user1Id, int user2Id);
        Task UnassignFriendAsync(int user1Id, int user2Id);
    }
}
