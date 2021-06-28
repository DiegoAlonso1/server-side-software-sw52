using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IFriendshipService
    {
        Task<IEnumerable<Person>> GetAllFriendsByPersonIdAsync(int personId);
        Task<FriendshipResponse> AssignFriendAsync(int principalId, int friendId);
        Task<FriendshipResponse> UnassignFriendAsync(int principalId, int friendId);
    }
}
