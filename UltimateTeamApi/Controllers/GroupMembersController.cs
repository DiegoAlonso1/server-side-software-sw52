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
    [Route("api/persons/{personId}/groups")]
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
            Summary = "Get All Groups By Person Id",
            Description = "Get List of All Groups By Person Id",
            OperationId = "GetAllGroupsByPersonId")]
        [SwaggerResponse(200, "List of Groups By Person Id", typeof(IEnumerable<GroupResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<GroupResource>> GetAllGroupsByPersonIdAsync(int personId)
        {
            var groups = await _groupMemberService.GetAllGroupsByPersonIdAsync(personId);
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
        public async Task<IActionResult> AssignGroupMemberAsync(int groupId, int personId, [FromBody] SaveGroupMemberResource resource)
        {
            var result = await _groupMemberService.AssignGroupMemberAsync(groupId, personId, resource.PersonCreator);

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
        public async Task<IActionResult> UnassignGroupMemberAsync(int groupId, int personId)
        {
            var result = await _groupMemberService.UnassignGroupMemberAsync(groupId, personId);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group, GroupResource>(result.Resource.Group);

            return Ok(groupResource);
        }
    }
}
