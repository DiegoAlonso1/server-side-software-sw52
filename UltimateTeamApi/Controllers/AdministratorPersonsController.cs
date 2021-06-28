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
    [Route("api/administrators/{administratorId}/persons")]
    [Produces("application/json")]
    [ApiController]
    public class AdministratorPersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public AdministratorPersonsController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        /******************************************/
            /*GET ALL PERSONS BY ADMIN ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Persons By Administrator Id",
            Description = "Get List of All Persons By Administrator Id",
            OperationId = "GetAllPersonsByAdministratorId")]
        [SwaggerResponse(200, "List of Persons By Administrator Id", typeof(IEnumerable<PersonResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<PersonResource>> GetAllByAdministratorIdAsync(int administratorId)
        {
            var persons = await _personService.GetAllByAdministratorIdAsync(administratorId);
            var resources = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(persons);
            return resources;
        }


    }

}
