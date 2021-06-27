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
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class SessionTypesController : ControllerBase
    {
        private readonly ISessionTypeService _sessionTypeService;
        private readonly IMapper _mapper;

        public SessionTypesController(ISessionTypeService functionalityService, IMapper mapper)
        {
            _sessionTypeService = functionalityService;
            _mapper = mapper;
        }



        /******************************************/
                    /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All SessionTypes",
            Description = "Get List of All SessionTypes",
            OperationId = "GetAllSessionTypes")]
        [SwaggerResponse(200, "List of SessionTypes", typeof(IEnumerable<SessionTypeResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SessionTypeResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SessionTypeResource>> GetAllAsync()
        {
            var sessionTypes = await _sessionTypeService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<SessionType>, IEnumerable<SessionTypeResource>>(sessionTypes);
            return resources;
        }



        /******************************************/
                /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get SessionType By Id",
            Description = "Get a SessionType By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "SessionType By Id", typeof(SessionTypeResource))]

        [HttpGet("{sessionTypeId}")]
        [ProducesResponseType(typeof(SessionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int sessionTypeId)
        {
            var result = await _sessionTypeService.GetByIdAsync(sessionTypeId);

            if (!result.Success)
                return BadRequest(result.Message);

            var sessionTypeResource = _mapper.Map<SessionType, SessionTypeResource>(result.Resource);
            return Ok(sessionTypeResource);
        }
    }
}
