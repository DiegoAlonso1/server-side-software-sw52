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
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService _sessionService;
        private readonly ISessionParticipantService _sessionParticipantService;
        private readonly IMapper _mapper;

        public SessionsController(ISessionService sessionService, IMapper mapper, ISessionParticipantService sessionParticipantService)
        {
            _sessionService = sessionService;
            _mapper = mapper;
            _sessionParticipantService = sessionParticipantService;
        }



        /******************************************/
                        /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Sessions",
            Description = "Get List of All Sessions",
            OperationId = "GetAllSessions")]
        [SwaggerResponse(200, "List of Sessions", typeof(IEnumerable<SessionResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionResource>> GetAllAsync()
        {
            var sessions = await _sessionService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }



        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Session By Id",
            Description = "Get a Session By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Session By Id", typeof(SessionResource))]

        [HttpGet("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int sessionId)
        {
            var result = await _sessionService.GetByIdAsync(sessionId);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(sessionResource);
        }



        /******************************************/
                  /*GET ALL BY NAME ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get All Sessions By Name",
            Description = "Get List of All Sessions By Name",
            OperationId = "GetAllByName")]
        [SwaggerResponse(200, "List of Sessions By Name", typeof(IEnumerable<SessionResource>))]

        [HttpGet("name/{sessionName}")]
        [ProducesResponseType(typeof(IEnumerable<SessionResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionResource>> GetByAllNameAsync(string sessionName)
        {
            var sessions = await _sessionService.GetAllByNameAsync(sessionName);
            var resources = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionResource>>(sessions);
            return resources;
        }



        /*************************************************/
        /*GET ALL SESSIONPARTICIPANTS BY SESSION ID ASYNC*/
        /*************************************************/
        [SwaggerOperation(
            Summary = "Get All SessionParticipants By Session Id",
            Description = "Get List of All SessionParticipants By Session Id",
            OperationId = "GetAllSessionParticipantsBySessionId")]
        [SwaggerResponse(200, "List of SessionParticipants By Session Id", typeof(IEnumerable<SessionParticipantResource>))]

        [HttpGet("{sessionId}/persons")]
        [ProducesResponseType(typeof(IEnumerable<SessionParticipantResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionParticipantResource>> GetByAllNameAsync(int sessionId)
        {
            var sessionParticipants = await _sessionParticipantService.GetAllBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<SessionParticipant>, IEnumerable<SessionParticipantResource>>(sessionParticipants);
            return resources;
        }



        /******************************************/
                      /*SAVE SESSION*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save Session",
            Description = "Create a Session",
            OperationId = "SaveSession")]
        [SwaggerResponse(200, "Session Created", typeof(SessionResource))]

        [HttpPost]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.SaveAsync(session);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(sessionResource);
        }



        /******************************************/
                     /*UPDATE SESSION*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Update Session",
           Description = "Update a Session",
           OperationId = "UpdateSession")]
        [SwaggerResponse(200, "Session Updated", typeof(SessionResource))]

        [HttpPut("{sessionId}")]
        [ProducesResponseType(typeof(SessionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int sessionId, [FromBody] SaveSessionResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var session = _mapper.Map<SaveSessionResource, Session>(resource);
            var result = await _sessionService.UpdateAsync(sessionId, session);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionResource = _mapper.Map<Session, SessionResource>(result.Resource);
            return Ok(sessionResource);
        }
    }
}
