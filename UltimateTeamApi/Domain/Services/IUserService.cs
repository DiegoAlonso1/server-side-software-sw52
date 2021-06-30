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
        Task <AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<IEnumerable<User>> GetAllAsync();
        Task<UserResponse> RegisterAsync(RegisterRequest userRequest);
        Task<UserResponse> UpdateAsync(int userId, AuthenticationRequest request);
        Task<UserResponse> DeleteAsync(int userId);

    }
}
