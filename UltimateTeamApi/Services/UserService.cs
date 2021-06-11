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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserResponse> DeleteAsync(int userId)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);

            if (existingUser == null)
                return new UserResponse("User not found");

            try
            {
                _userRepository.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while deleting the user: {ex.Message}");
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.ListAsync();
        }

        public async Task<IEnumerable<User>> GetAllByAdministratorIdAsync(int administratorId)
        {
            return await _userRepository.ListByAdministratorIdAsync(administratorId);
        }

        public async Task<UserResponse> GetByIdAsync(int userId)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);

            if (existingUser == null)
                return new UserResponse("User not found");

            return new UserResponse(existingUser);
        }

        public async Task<UserResponse> SaveAsync(User user)
        {
            try
            {
                await _userRepository.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while saving the user: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateAsync(int userId, User userRequest)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);

            if (existingUser == null)
                return new UserResponse("User not found");

            existingUser.Name = userRequest.Name;
            existingUser.LastName = userRequest.LastName;
            existingUser.UserName = userRequest.UserName;
            existingUser.Email = userRequest.Email;
            existingUser.Password = userRequest.Password;
            existingUser.Birthdate = userRequest.Birthdate;
            existingUser.LastConnection = userRequest.LastConnection;
            existingUser.ProfilePicture = userRequest.ProfilePicture;
            existingUser.AdministratorId = userRequest.AdministratorId;

            try
            {
                _userRepository.Update(existingUser);
                await _unitOfWork.CompleteAsync();

                return new UserResponse(existingUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"An error ocurred while updating the user: {ex.Message}");
            }
        }
    }
}
