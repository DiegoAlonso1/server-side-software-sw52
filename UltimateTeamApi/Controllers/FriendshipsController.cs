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
    [Route("api/persons/{principalId}/friends")]
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
        /*GET ALL FRIENDS BY PERSON ID ASYNC*/
        /**********************************************/

        [SwaggerOperation(
           Summary = "Get All Friends By Person Id",
           Description = "Get a List of All Friends By Person Id",
           OperationId = "GetAllFriendsByPersonId")]
        [SwaggerResponse(200, "Friends By PersonId", typeof(IEnumerable<PersonResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<PersonResource>> GetAllFriendsByPersonIdAsync(int principalId)
        {
            var friends = await _friendshipService.GetAllFriendsByPersonIdAsync(principalId);
            var resources = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(friends);
            return resources;
        }



        /*****************************************************************/
        /*ASSIGN FRIENDSHIP*/
        /*****************************************************************/

        [SwaggerOperation(
           Summary = "Assign Friendship",
           Description = "Assign Friendship",
           OperationId = "Assign Friendship")]
        [SwaggerResponse(200, "Friendship Assigned", typeof(PersonResource))]

        [HttpPost("{friendId}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignFriendAsync(int principalId, int friendId)
        {
            var result = await _friendshipService.AssignFriendAsync(principalId, friendId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipResource = _mapper.Map<Person, PersonResource>(result.Resource.Friend);
            return Ok(friendshipResource);
        }

        /*****************************************************************/
        /*UNASSIGN FRIENDSHIP*/
        /*****************************************************************/

        [SwaggerOperation(
          Summary = "Unassign Friendship",
          Description = "Unassign Friendship",
          OperationId = "Unassign Friendship")]
        [SwaggerResponse(200, "Friendship Unassigned", typeof(PersonResource))]

        [HttpDelete("{friendId}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignFriendAsync(int principalId, int friendId)
        {
            var result = await _friendshipService.UnassignFriendAsync(principalId, friendId);

            if (!result.Success)
                return BadRequest(result.Message);

            var friendshipResource = _mapper.Map<Person, PersonResource>(result.Resource.Friend);
            return Ok(friendshipResource);
        }

    }
}
