using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IGroupMemberService _groupMemberService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupService groupService, IMapper mapper, IGroupMemberService groupMemberService)
        {
            _groupService = groupService;
            _mapper = mapper;
            _groupMemberService = groupMemberService;
        }

        /******************************************/
        /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Groups",
            Description = "Get List of All Groups",
            OperationId = "GetAllGroups")]
        [SwaggerResponse(200, "List of Groups", typeof(IEnumerable<GroupResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<GroupResource>> GetAllAsync()
        {
            var groups = await _groupService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupResource>>(groups);
            return resources;
        }



        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Group By Id",
            Description = "Get a Group By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Group By Id", typeof(GroupResource))]

        [HttpGet("{groupId}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int groupId)
        {
            var result = await _groupService.GetByIdAsync(groupId);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group, GroupResource>(result.Resource);
            return Ok(groupResource);
        }



        /******************************************/
                    /*SAVE GROUP*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save Group",
            Description = "Create a Group",
            OperationId = "SaveGroup")]
        [SwaggerResponse(200, "Group Created", typeof(GroupResource))]

        [HttpPost]
        [ProducesResponseType(typeof(GroupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveGroupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var group = _mapper.Map<SaveGroupResource, Group>(resource);
            var result = await _groupService.SaveAsync(group);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group, GroupResource>(result.Resource);
            return Ok(groupResource);
        }



        /******************************************/
                    /*UPDATE GROUP*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Update Group",
           Description = "Update a Group",
           OperationId = "UpdateGroup")]
        [SwaggerResponse(200, "Group Updated", typeof(GroupResource))]

        [HttpPut("{groupId}")]
        [ProducesResponseType(typeof(GroupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int groupId, [FromBody] SaveGroupResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var group = _mapper.Map<SaveGroupResource, Group>(resource);
            var result = await _groupService.UpdateAsync(groupId, group);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group, GroupResource>(result.Resource);
            return Ok(groupResource);
        }


        /******************************************/
                    /*DELETE GROUP*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete Group",
           Description = "Delete a Group",
           OperationId = "DeleteGroup")]
        [SwaggerResponse(200, "Group Deleted", typeof(GroupResource))]

        [HttpDelete("{groupId}")]
        [ProducesResponseType(typeof(GroupResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int groupId)
        {
            var result = await _groupService.DeleteAsync(groupId);

            if (!result.Success)
                return BadRequest(result.Message);

            var groupResource = _mapper.Map<Group, GroupResource>(result.Resource);
            return Ok(groupResource);
        }



        /****************************************************/
                /*GET ALL MEMBERS BY GROUP ID ASYNC*/
        /****************************************************/

        [SwaggerOperation(
            Summary = "Get All Members By Group Id",
            Description = "Get List of All Members By Group Id",
            OperationId = "GetAllMembersByGroupId")]
        [SwaggerResponse(200, "Members", typeof(IEnumerable<PersonResource>))]

        [HttpGet("{groupId}/groupMembers")]
        [ProducesResponseType(typeof(IEnumerable<PersonResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<PersonResource>> GetAllMembersByGroupIdAsync(int groupId)
        {
            var members = await _groupMemberService.GetAllPersonsByGroupIdAsync(groupId);
            var resources = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(members);
            return resources;
        }
    }
}
