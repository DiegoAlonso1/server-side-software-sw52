using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services.Communications;

namespace UltimateTeamApi.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetAllByAdministratorIdAsync(int administratorId);
        Task<UserResponse> GetByIdAsync(int userId);
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(int userId, User userRequest);
        Task<UserResponse> DeleteAsync(int userId);
    }
}
