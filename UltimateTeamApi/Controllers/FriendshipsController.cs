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
    [Route("api/users/{user1Id}/friends")]
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
        /*GET ALL FRIENDSHIPS BY USER ID ASYNC*/
        /**********************************************/

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<UserResource>> GetAllByUserIdAsync(int user1Id)
        {
            var friendships = await _friendshipService.GetAllByUserIdAsync(user1Id);
            var resources = _mapper.Map<IEnumerable<Friendship>, IEnumerable<UserResource>>(friendships);
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

        [HttpPost("{user2Id}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignFriendAsync(int user1Id, int user2Id)
        {
            var result = await _friendshipService.AssignFriendAsync(user1Id, user2Id);

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

        [HttpDelete("{user2Id}")]
        [ProducesResponseType(typeof(UserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignFriendAsync(int user1Id, int user2Id)
        {
            var result = await _friendshipService.UnassignFriendAsync(user1Id, user2Id);
            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipResource = _mapper.Map<User, UserResource>(result.Resource.Principal);
            return Ok(friendshipResource);
        }

    }
}
