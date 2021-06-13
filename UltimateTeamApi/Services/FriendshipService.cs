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

        public async Task<FriendshipResponse> AssignFriendAsync(int user1Id, int user2Id)
        {
            try
            {
                await _friendshipRepository.AssignFriendAsync(user1Id, user2Id);
                await _unitOfWork.CompleteAsync();
                var friendship = await _friendshipRepository.FindByUser1IdAndUser2IdAsync(user1Id, user2Id);
                return new FriendshipResponse(friendship);
            }
            catch (Exception ex)
            {
                return new FriendshipResponse($"An error ocurred while assigning a Friendship: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Friendship>> GetAllByUserIdAsync(int userId)
        {
            return await _friendshipRepository.ListByUserIdAsync(userId);
        }

        public async Task<FriendshipResponse> UnassignFriendAsync(int user1Id, int user2Id)
        {
            try
            {
                Friendship friendship = await _friendshipRepository.FindByUser1IdAndUser2IdAsync(user1Id, user2Id);
                if (friendship == null) throw new Exception();
                await _friendshipRepository.UnassignFriendAsync(user1Id, user2Id);
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
