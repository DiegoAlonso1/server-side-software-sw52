using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;

namespace UltimateTeamApi.Domain.Persistance.Repositories
{
    public interface IFriendshipRepository
    {
        Task<IEnumerable<User>> ListFriendsByUserIdAsync(int userId);
        Task<Friendship> FindByPrincipalIdAndFriendIdAsync(int principalId, int friendId);
        Task AddAsync(Friendship friendship);
        void Remove(Friendship friendship);
        Task AssignFriendAsync(int principalId, int friendId);
        Task UnassignFriendAsync(int principalId, int friendId);
    }
}
