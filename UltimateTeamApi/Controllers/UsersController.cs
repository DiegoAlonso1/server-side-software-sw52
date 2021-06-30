using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Domain.Services.Communications;
using UltimateTeamApi.Extensions;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }



        /******************************************/
                      /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Users",
            Description = "Get List of All Users",
            OperationId = "GetAllUsers")]
        [SwaggerResponse(200, "List of Users", typeof(IEnumerable<UserResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<UserResource>> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            return resources;
        }



        /******************************************/
                    /*AUTHENTICATE USER*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Authenticate User",
            Description = "Authenticate a User",
            OperationId = "AuthenticateUser")]
        [SwaggerResponse(200, "User Authenticated", typeof(AuthenticationResponse))]

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _userService.AuthenticateAsync(request);

            if (result == null)
                return BadRequest("Invalid username or password");

            return Ok(result);
        }



        /******************************************/
                    /*REGISTER USER*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Register User",
            Description = "Register a User",
            OperationId = "RegisterUser")]
        [SwaggerResponse(200, "User Registered", typeof(string))]

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _userService.RegisterAsync(request);

            if (result == null)
                return BadRequest("Invalid username or password");

            return Ok("Registration succesful");
        }



        /******************************************/
                    /*UPDATE USER*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Update User",
            Description = "Update a User",
            OperationId = "UpdateUser")]
        [SwaggerResponse(200, "User Updated", typeof(UserResource))]

        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateAsync(int userId, [FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _userService.UpdateAsync(userId, request);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }



        /******************************************/
                    /*DELETE USER*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete User",
           Description = "Delete a User",
           OperationId = "DeleteUser")]
        [SwaggerResponse(200, "User Deleted", typeof(UserResource))]

        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int userId)
        {
            var result = await _userService.DeleteAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }
    }
}
