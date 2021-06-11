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
    [Route("api/sessions/{sessionId}/stadistics")]
    [Produces("application/json")]
    [ApiController]
    public class SessionStadisticsController : ControllerBase
    {
        private readonly ISessionStadisticService _sessionStadisticService;
        private readonly IMapper _mapper;

        public SessionStadisticsController(ISessionStadisticService sessionStadisticService, IMapper mapper)
        {
            _sessionStadisticService = sessionStadisticService;
            _mapper = mapper;
        }



        /**********************************************/
        /*GET ALL SESIONSTADISTICS BY SESSION ID ASYNC*/
        /**********************************************/

        [SwaggerOperation(
            Summary = "Get All SessionStadistics By Session Id",
            Description = "Get List of All SessionStadistics By Session Id",
            OperationId = "GetAllSessionStadisticsBySessionId")]
        [SwaggerResponse(200, "SessionStadistics", typeof(IEnumerable<SessionStadisticResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionStadisticResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionStadisticResource>> GetAllSessionStadisticsBySessionIdAsync(int sessionId)
        {
            var sessionStadistics = await _sessionStadisticService.GetAllStadisticsBySessionIdAsync(sessionId);
            var resources = _mapper.Map<IEnumerable<SessionStadistic>, IEnumerable<SessionStadisticResource>>(sessionStadistics);
            return resources;
        }



        /******************************************/
               /*ASSIGN SESSIONSTADISTIC ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Assign SessionStadistic",
            Description = "Assign a Session with a Functionality",
            OperationId = "AssignSessionStadistic")]
        [SwaggerResponse(200, "SessionStadistic Assigned", typeof(SessionStadisticResource))]

        [HttpGet("{functionalityId}")]
        [ProducesResponseType(typeof(SessionStadisticResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> AssignSessionStadisticAsync(int sessionId, int functionalityId)
        {
            var result = await _sessionStadisticService.AssignSessionStadisticAsync(sessionId, functionalityId);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionStadisticResource = _mapper.Map<SessionStadistic, SessionStadisticResource>(result.Resource);
            return Ok(sessionStadisticResource);
        }
    }
}
