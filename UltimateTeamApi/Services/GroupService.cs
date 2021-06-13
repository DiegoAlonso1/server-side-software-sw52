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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUnitOfWork _unitOfWork;
        public GroupService(IGroupRepository groupRepository, IUnitOfWork unitOfWork)
        {
            _groupRepository = groupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GroupResponse> DeleteAsync(int groupId)
        {
            var existingGroup = await _groupRepository.FindByIdAsync(groupId);

            if (existingGroup == null)
                return new GroupResponse("Group not found");

            try
            {
                _groupRepository.Remove(existingGroup);
                await _unitOfWork.CompleteAsync();

                return new GroupResponse(existingGroup);
            }
            catch (Exception ex)
            {
                return new GroupResponse($"An error ocurred while deleting a group: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await _groupRepository.ListAsync();
        }

        public async Task<GroupResponse> GetByIdAsync(int groupId)
        {
            var existingGroup = await _groupRepository.FindByIdAsync(groupId);

            if (existingGroup == null)
                return new GroupResponse("Group not found");

            return new GroupResponse(existingGroup);
        }

        public async Task<GroupResponse> SaveAsync(Group group)
        {
            try
            {
                await _groupRepository.AddAsync(group);
                await _unitOfWork.CompleteAsync();

                return new GroupResponse(group);
            }
            catch (Exception ex)
            {
                return new GroupResponse($"An error ocurred while saving a group: {ex.Message}");
            }
        }

        public async Task<GroupResponse> UpdateAsync(int groupId, Group groupRequest)
        {
            var existingGroup = await _groupRepository.FindByIdAsync(groupId);

            if (existingGroup == null)
                return new GroupResponse("Group not found");

            existingGroup.Name = groupRequest.Name;

            try
            {
                _groupRepository.Update(existingGroup);
                await _unitOfWork.CompleteAsync();

                return new GroupResponse(existingGroup);
            }
            catch (Exception ex)
            {
                return new GroupResponse($"An error ocurred while updating a group: {ex.Message}");
            }
        }
    }
}
