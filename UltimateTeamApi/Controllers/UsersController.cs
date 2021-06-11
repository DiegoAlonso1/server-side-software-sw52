using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Extensions;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Controllers
{
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
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get User By Id",
            Description = "Get a User By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "User By Id", typeof(UserResource))]

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int userId)
        {
            var result = await _userService.GetByIdAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
        }



        /******************************************/
                        /*SAVE USER*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save User",
            Description = "Create a User",
            OperationId = "SaveUser")]
        [SwaggerResponse(200, "User Created", typeof(UserResource))]

        [HttpPost]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.SaveAsync(user);

            if (!result.Success)
                return BadRequest(result.Message);

            var userResource = _mapper.Map<User, UserResource>(result.Resource);
            return Ok(userResource);
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
        public async Task<IActionResult> PutAsync(int userId, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var user = _mapper.Map<SaveUserResource, User>(resource);
            var result = await _userService.UpdateAsync(userId, user);

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
