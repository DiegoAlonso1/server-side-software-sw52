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
    [Route("api/users/{userId}/groups")]
    [Produces("application/json")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly IGroupMemberService _groupMemberService;
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;
        public GroupMembersController(IGroupMemberService groupMemberService, IMapper mapper, IGroupService groupService)
        {
            _groupMemberService = groupMemberService;
            _mapper = mapper;
            _groupService = groupService;
        }

        /******************************************/
        /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Groups By User Id",
            Description = "Get List of All Groups By User Id",
            OperationId = "GetAllGroupsByUserId")]
        [SwaggerResponse(200, "List of Groups By User Id", typeof(IEnumerable<GroupResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<GroupResource>> GetAllGroupsByUserIdAsync(int userId)
        {
            var groups = await _groupMemberService.GetAllGroupsByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return resources;
        }



        /******************************************/
        /*SAVE GROUPMEMBER*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save GroupMember",
            Description = "Create a GroupMember",
            OperationId = "SaveGroupMember")]
        [SwaggerResponse(200, "GroupMember Created", typeof(GroupMemberResource))]

        [HttpPost("{groupId}")]
        [ProducesResponseType(typeof(GroupMemberResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignGroupMemberAsync(int groupId, int userId, [FromBody] SaveGroupMemberResource resource)
        {
            var result = await _groupMemberService.AssignGroupMemberAsync(groupId, userId, resource.UserCreator);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group,GroupResource>(result.Resource.Group);

            return Ok(groupResource);
        }



        /******************************************/
        /*DELETE GROUPMEMBER*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete GroupMember",
           Description = "Delete a GroupMember",
           OperationId = "DeleteGroupMember")]
        [SwaggerResponse(200, "GroupMember Deleted", typeof(GroupResource))]

        [HttpDelete("{groupId}")]
        [ProducesResponseType(typeof(GroupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UnassignGroupMemberAsync(int groupId, int userId)
        {
            var result = await _groupMemberService.UnassignGroupMemberAsync(groupId, userId);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group, GroupResource>(result.Resource.Group);

            return Ok(groupResource);
        }
    }
}
