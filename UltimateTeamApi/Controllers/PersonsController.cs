using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
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
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonsController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }



        /******************************************/
                        /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Persons",
            Description = "Get List of All Persons",
            OperationId = "GetAllPersons")]
        [SwaggerResponse(200, "List of Persons", typeof(IEnumerable<PersonResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<PersonResource>> GetAllAsync()
        {
            var persons = await _personService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(persons);
            return resources;
        }



        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Person By Id",
            Description = "Get a Person By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Person By Id", typeof(PersonResource))]

        [HttpGet("{personId}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int personId)
        {
            var result = await _personService.GetByIdAsync(personId);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, PersonResource>(result.Resource);
            return Ok(personResource);
        }



        /******************************************/
                    /*GET BY EMAIL ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Person By Email",
            Description = "Get a Person By Email",
            OperationId = "GetByEmail")]
        [SwaggerResponse(200, "Person By Email", typeof(PersonResource))]

        [HttpGet("email/{personEmail}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByEmailAsync(string personEmail)
        {
            var result = await _personService.GetByEmailAsync(personEmail);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, PersonResource>(result.Resource);
            return Ok(personResource);
        }



        /******************************************/
                        /*SAVE PERSON*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save Person",
            Description = "Create a Person",
            OperationId = "SavePerson")]
        [SwaggerResponse(200, "Person Created", typeof(PersonResource))]

        [HttpPost]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SavePersonResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var person = _mapper.Map<SavePersonResource, Person>(resource);
            var result = await _personService.SaveAsync(person);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, PersonResource>(result.Resource);
            return Ok(personResource);
        }



        /******************************************/
                        /*UPDATE PERSON*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Update Person",
           Description = "Update a Person",
           OperationId = "UpdatePerson")]
        [SwaggerResponse(200, "Person Updated", typeof(PersonResource))]

        [HttpPut("{personId}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int personId, [FromBody] SavePersonResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var person = _mapper.Map<SavePersonResource, Person>(resource);
            var result = await _personService.UpdateAsync(personId, person);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, PersonResource>(result.Resource);
            return Ok(personResource);
        }



        /******************************************/
                        /*DELETE PERSON*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete Person",
           Description = "Delete a Person",
           OperationId = "DeletePerson")]
        [SwaggerResponse(200, "Person Deleted", typeof(PersonResource))]

        [HttpDelete("{personId}")]
        [ProducesResponseType(typeof(PersonResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int personId)
        {
            var result = await _personService.DeleteAsync(personId);

            if (!result.Success)
                return BadRequest(result.Message);

            var personResource = _mapper.Map<Person, PersonResource>(result.Resource);
            return Ok(personResource);
        }
    }
}
