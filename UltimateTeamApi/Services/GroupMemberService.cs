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
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IGroupMemberRepository _groupMemberRepository;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GroupMemberService(IGroupMemberRepository groupMemberRepository, IUnitOfWork unitOfWork, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupMemberRepository = groupMemberRepository;
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public async Task<GroupMemberResponse> AssignGroupMemberAsync(int groupId, int userId)
        {
            try
            {
                await _groupMemberRepository.AssignGroupMemberAsync(groupId, userId);
                await _unitOfWork.CompleteAsync();
                GroupMember groupMember = await _groupMemberRepository.FindByGroupIdAndUserIdAsync(groupId, userId);
                return new GroupMemberResponse(groupMember);

            }
            catch (Exception ex)
            {
                return new GroupMemberResponse($"An error ocurred while assigning Member to Group: {ex.Message}");
            }
        }

        public async Task<IEnumerable<GroupMember>> GetAllByGroupIdAsync(int groupId)
        {
            return await _groupMemberRepository.ListByGroupIdAsync(groupId);
        }

        public async Task<IEnumerable<GroupMember>> GetAllByUserIdAsync(int userId)
        {
            return await _groupMemberRepository.ListByUserIdAsync(userId);
        }

        public async Task<GroupMemberResponse> UnassignGroupMemberAsync(int groupId, int userId)
        {
            try
            {
                GroupMember groupMember = await _groupMemberRepository.FindByGroupIdAndUserIdAsync(groupId, userId);

                _groupMemberRepository.Remove(groupMember);
                await _unitOfWork.CompleteAsync();

                return new GroupMemberResponse(groupMember);

            }
            catch (Exception ex)
            {
                return new GroupMemberResponse($"An error ocurred while unassigning Member from Group: {ex.Message}");
            }
        }
    }
}
