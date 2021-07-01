using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Persistance.Repositories;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Domain.Services.Communications;
using UltimateTeamApi.Exceptions;
using UltimateTeamApi.Settings;
using BCryptNet = BCrypt.Net;

namespace UltimateTeamApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var users = await _userRepository.ListAsync();
            var user = users.SingleOrDefault(x => x.Username == request.Username);

            if (user == null || !BCryptNet.BCrypt.Verify(request.Password, user.Password)) 
                throw new AppCustomException("Username or password is incorrect");

            var token = generateJwtToken(user);
            return new AuthenticationResponse(user, token);
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

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var users = await _userRepository.ListAsync();

            if (users.Any(x => x.Username == request.Username))
            {
                throw new AppCustomException($"Username '{request.Username}' is already taken");
            }

            var user = _mapper.Map<RegisterRequest, User>(request);
            user.Password = BCryptNet.BCrypt.HashPassword(request.Password);

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return new UserResponse(user);
        }

        public async Task<UserResponse> UpdateAsync(int userId, AuthenticationRequest request)
        {
            var existingUser = await _userRepository.FindByIdAsync(userId);

            if (existingUser == null)
                return new UserResponse("User not found");

            if (request.Username != existingUser.Username)
            {
                var users = await _userRepository.ListAsync();

                if (users.Any(x => x.Username == request.Username))
                {
                    return new UserResponse($"Username '{request.Username}' is already taken");
                }
            }

            existingUser.Username = request.Username;
            existingUser.Password = request.Password;

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

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
