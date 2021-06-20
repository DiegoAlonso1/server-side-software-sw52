using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FriendshipService(IFriendshipRepository friendshipRepository, IUnitOfWork unitOfWork)
        {
            _friendshipRepository = friendshipRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<FriendshipResponse> AssignFriendAsync(int principalId, int friendId)
        {
            try
            {
                await _friendshipRepository.AssignFriendAsync(principalId, friendId);
                await _unitOfWork.CompleteAsync();
                var friendship = await _friendshipRepository.FindByPrincipalIdAndFriendIdAsync(principalId, friendId);
                return new FriendshipResponse(friendship);
            }
            catch (Exception ex)
            {
                return new FriendshipResponse($"An error ocurred while assigning a Friendship: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetAllFriendsByUserIdAsync(int userId)
        {
            return await _friendshipRepository.ListFriendsByUserIdAsync(userId);
        }

        public async Task<FriendshipResponse> UnassignFriendAsync(int principalId, int friendId)
        {
            try
            {
                Friendship friendship = await _friendshipRepository.FindByPrincipalIdAndFriendIdAsync(principalId, friendId);
                if (friendship == null) throw new Exception();
                await _friendshipRepository.UnassignFriendAsync(principalId, friendId);
                await _unitOfWork.CompleteAsync();
                return new FriendshipResponse(friendship);
            }
            catch (Exception ex)
            {
                return new FriendshipResponse($"An error ocurred while unassigning a Friendship: {ex.Message}");
            }
        }
    }
}
