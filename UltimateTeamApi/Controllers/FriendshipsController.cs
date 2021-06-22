using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.Domain.Models;
using UltimateTeamApi.Domain.Services;
using UltimateTeamApi.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/users/{principalId}/friends")]
    [Produces("application/json")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IMapper _mapper;

        public FriendshipsController(IFriendshipService friendshipService, IMapper mapper)
        {
            _friendshipService = friendshipService;
            _mapper = mapper;
        }

        /**********************************************/
        /*GET ALL FRIENDS BY USER ID ASYNC*/
        /**********************************************/

        [SwaggerOperation(
           Summary = "Get All Friends By UserId",
           Description = "Get a List of All Friends By User Id",
           OperationId = "GetAllFriendsByUserId")]
        [SwaggerResponse(200, "Friends By UserId", typeof(IEnumerable<UserResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<UserResource>> GetAllFriendsByUserIdAsync(int principalId)
        {
            var friends = await _friendshipService.GetAllFriendsByUserIdAsync(principalId);
            var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(friends);
            return resources;
        }



        /*****************************************************************/
        /*ASSIGN FRIENDSHIP*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Assign Friendship",
           Description = "Assign Friendship",
           OperationId = "Assign Friendship")]
        [SwaggerResponse(200, "Friendship Assigned", typeof(UserResource))]

        [HttpPost("{friendId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignFriendAsync(int principalId, int friendId)
        {
            var result = await _friendshipService.AssignFriendAsync(principalId, friendId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipResource = _mapper.Map<User, UserResource>(result.Resource.Friend);
            return Ok(friendshipResource);
        }

        /*****************************************************************/
        /*UNASSIGN FRIENDSHIP*/
        /*****************************************************************/

        [SwaggerOperation(
          Summary = "Unassign Friendship",
          Description = "Unassign Friendship",
          OperationId = "Unassign Friendship")]
        [SwaggerResponse(200, "Friendship Unassigned", typeof(UserResource))]

        [HttpDelete("{friendId}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignFriendAsync(int principalId, int friendId)
        {
            var result = await _friendshipService.UnassignFriendAsync(principalId, friendId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipResource = _mapper.Map<User, UserResource>(result.Resource.Friend);
            return Ok(friendshipResource);
        }

    }
}
