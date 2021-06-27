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
    [Route("api/users/{userId}/sessions")]
    [Produces("application/json")]
    [ApiController]
    public class SessionParticipantsController : ControllerBase
    {
        private readonly ISessionParticipantService _sessionParticipantService;
        private readonly IMapper _mapper;

        public SessionParticipantsController(ISessionParticipantService sessionParticipantService, IMapper mapper)
        {
            _sessionParticipantService = sessionParticipantService;
            _mapper = mapper;
        }



        /*************************************************/
          /*GET ALL SESSIONPARTICIPANTS BY USER ID ASYNC*/
        /*************************************************/

        [SwaggerOperation(
            Summary = "Get All SessionParticipants By User Id",
            Description = "Get List of All SessionParticipants By User Id",
            OperationId = "GetAllSessionParticipantsByUserId")]
        [SwaggerResponse(200, "SessionParticipants By User Id", typeof(IEnumerable<SessionParticipantResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionParticipantResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionParticipantResource>> GetAllSessionParticipantsByUserIdAsync(int userId)
        {
            var sessionParticipants = await _sessionParticipantService.GetAllByUserIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<SessionParticipant>, IEnumerable<SessionParticipantResource>>(sessionParticipants);
            return resources;
        }



        /******************************************************/
        /*GET ALL SESSIONPARTICIPANTS BY USER CREATOR ID ASYNC*/
        /******************************************************/

        [SwaggerOperation(
            Summary = "Get All SessionParticipants By User Creator Id",
            Description = "Get List of All SessionParticipants By User Creator Id",
            OperationId = "GetAllSessionParticipantsByUserCreatorId")]
        [SwaggerResponse(200, "SessionParticipants By User Creator Id", typeof(IEnumerable<SessionParticipantResource>))]

        [HttpGet("creator")]
        [ProducesResponseType(typeof(IEnumerable<SessionParticipantResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionParticipantResource>> GetAllSessionParticipantsByUserCreatorIdAsync(int userId)
        {
            var sessionParticipants = await _sessionParticipantService.GetAllByUserCreatorIdAsync(userId);
            var resources = _mapper.Map<IEnumerable<SessionParticipant>, IEnumerable<SessionParticipantResource>>(sessionParticipants);
            return resources;
        }



        /******************************************/
            /*ASSIGN SESSIONPARTICIPANT ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Assign SessionParticipant",
            Description = "Assign a Session with a User",
            OperationId = "AssignSessionParticipant")]
        [SwaggerResponse(200, "SessionParticipant Assigned", typeof(SessionParticipantResource))]

        [HttpPost("{sessionId}")]
        [ProducesResponseType(typeof(SessionParticipantResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignSessionStadisticAsync(int sessionId, int userId, [FromBody] SaveSessionParticipantResource resource)
        {
            var result = await _sessionParticipantService.AssignSessionParticipantAsync(sessionId, userId, resource.Creator);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionParticipantResource = _mapper.Map<SessionParticipant, SessionParticipantResource>(result.Resource);
            return Ok(sessionParticipantResource);
        }
    }
}
