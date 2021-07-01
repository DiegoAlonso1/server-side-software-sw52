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
    [Route("api/persons/{personId}/sessions")]
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
          /*GET ALL SESSIONPARTICIPANTS BY PERSON ID ASYNC*/
        /*************************************************/

        [SwaggerOperation(
            Summary = "Get All SessionParticipants By Person Id",
            Description = "Get List of All SessionParticipants By Person Id",
            OperationId = "GetAllSessionParticipantsByPersonId")]
        [SwaggerResponse(200, "SessionParticipants By Person Id", typeof(IEnumerable<SessionParticipantResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionParticipantResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionParticipantResource>> GetAllSessionParticipantsByPersonIdAsync(int personId)
        {
            var sessionParticipants = await _sessionParticipantService.GetAllByPersonIdAsync(personId);
            var resources = _mapper.Map<IEnumerable<SessionParticipant>, IEnumerable<SessionParticipantResource>>(sessionParticipants);
            return resources;
        }



        /********************************************************/
        /*GET ALL SESSIONPARTICIPANTS BY PERSON CREATOR ID ASYNC*/
        /********************************************************/

        [SwaggerOperation(
            Summary = "Get All SessionParticipants By Person Creator Id",
            Description = "Get List of All SessionParticipants By Person Creator Id",
            OperationId = "GetAllSessionParticipantsByPersonCreatorId")]
        [SwaggerResponse(200, "SessionParticipants By Person Creator Id", typeof(IEnumerable<SessionParticipantResource>))]

        [HttpGet("creator")]
        [ProducesResponseType(typeof(IEnumerable<SessionParticipantResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionParticipantResource>> GetAllSessionParticipantsByPersonCreatorIdAsync(int personId)
        {
            var sessionParticipants = await _sessionParticipantService.GetAllByPersonCreatorIdAsync(personId);
            var resources = _mapper.Map<IEnumerable<SessionParticipant>, IEnumerable<SessionParticipantResource>>(sessionParticipants);
            return resources;
        }



        /******************************************/
            /*ASSIGN SESSIONPARTICIPANT ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Assign SessionParticipant",
            Description = "Assign a Session with a Person",
            OperationId = "AssignSessionParticipant")]
        [SwaggerResponse(200, "SessionParticipant Assigned", typeof(SessionParticipantResource))]

        [HttpPost("{sessionId}")]
        [ProducesResponseType(typeof(SessionParticipantResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignSessionStadisticAsync(int sessionId, int personId, [FromBody] SaveSessionParticipantResource resource)
        {
            var result = await _sessionParticipantService.AssignSessionParticipantAsync(sessionId, personId, resource.Creator);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionParticipantResource = _mapper.Map<SessionParticipant, SessionParticipantResource>(result.Resource);
            return Ok(sessionParticipantResource);
        }
    }
}
