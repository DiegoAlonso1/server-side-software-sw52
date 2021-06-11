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
    public class FunctionalitiesController : ControllerBase
    {
        private readonly IFunctionalityService _functionalityService;
        private readonly IMapper _mapper;

        public FunctionalitiesController(IFunctionalityService functionalityService, IMapper mapper)
        {
            _functionalityService = functionalityService;
            _mapper = mapper;
        }



        /******************************************/
                    /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Functionalities",
            Description = "Get List of All Functionalities",
            OperationId = "GetAllFunctionalities")]
        [SwaggerResponse(200, "List of Functionalities", typeof(IEnumerable<FunctionalityResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FunctionalityResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<FunctionalityResource>> GetAllAsync()
        {
            var functionalities = await _functionalityService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<Functionality>, IEnumerable<FunctionalityResource>>(functionalities);
            return resources;
        }



        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Functionality By Id",
            Description = "Get a Functionality By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Functionality By Id", typeof(FunctionalityResource))]

        [HttpGet("{functionalityId}")]
        [ProducesResponseType(typeof(FunctionalityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int functionalityId)
        {
            var result = await _functionalityService.GetByIdAsync(functionalityId);

            if (!result.Success)
                return BadRequest(result.Message);

            var functionalityResource = _mapper.Map<Functionality, FunctionalityResource>(result.Resource);
            return Ok(functionalityResource);
        }
    }
}
