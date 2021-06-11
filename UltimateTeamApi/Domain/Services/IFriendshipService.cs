using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    interface IFriendshipService
    {
        Task<IEnumerable<Friendship>> GetAllByUserIdAsync(int userId);
        Task<FriendshipResponse> GetByUser1IdAndUser2IdAsync(int user1Id, int user2Id);
        Task<FriendshipResponse> AssignFriendAsync(int user1Id, int user2Id);
        Task<FriendshipResponse> UnassignFriendAsync(int user1Id, int user2Id);
    }
}
