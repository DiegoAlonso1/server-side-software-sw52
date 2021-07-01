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
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GroupMemberService(IGroupMemberRepository groupMemberRepository, IUnitOfWork unitOfWork, IGroupRepository groupRepository, IPersonRepository personRepository)
        {
            _groupMemberRepository = groupMemberRepository;
            _unitOfWork = unitOfWork;
            _groupRepository = groupRepository;
            _personRepository = personRepository;
        }

        public async Task<GroupMemberResponse> AssignGroupMemberAsync(int groupId, int personId, bool personCreator)
        {
            try
            {
                await _groupMemberRepository.AssignGroupMemberAsync(groupId, personId, personCreator);
                await _unitOfWork.CompleteAsync();
                GroupMember groupMemberResult = await _groupMemberRepository.FindByGroupIdAndPersonIdAsync(groupId, personId);
                return new GroupMemberResponse(groupMemberResult);

            }
            catch (Exception ex)
            {
                return new GroupMemberResponse($"An error ocurred while assigning Member to Group: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersonsByGroupIdAsync(int groupId)
        {
            return await _groupMemberRepository.ListPersonsByGroupIdAsync(groupId);
        }

        public async Task<IEnumerable<Group>> GetAllGroupsByPersonIdAsync(int personId)
        {
            return await _groupMemberRepository.ListGroupsByPersonIdAsync(personId);
        }

        public async Task<GroupMemberResponse> UnassignGroupMemberAsync(int groupId, int personId)
        {
            try
            {
                GroupMember groupMember = await _groupMemberRepository.FindByGroupIdAndPersonIdAsync(groupId, personId);

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
