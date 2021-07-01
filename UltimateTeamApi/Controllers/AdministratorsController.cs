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
    public class AdministratorsController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;
        private readonly IMapper _mapper;

        public AdministratorsController(IAdministratorService administratorService, IMapper mapper)
        {
            _administratorService = administratorService;
            _mapper = mapper;
        }

        /******************************************/
                    /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Admins",
            Description = "Get List of All Admins",
            OperationId = "GetAllAdmins")]
        [SwaggerResponse(200, "List of Admins", typeof(IEnumerable<AdministratorResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdministratorResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<AdministratorResource>> GetAllAsync()
        {
            var admins = await _administratorService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<Administrator>, IEnumerable<AdministratorResource>>(admins);
            return resources;
        }

        /******************************************/
                    /*GET ALL BY AREA ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Admins By Area",
            Description = "Get List of All Admins By Area",
            OperationId = "GetAllAdminsByArea")]
        [SwaggerResponse(200, "List of Admins by Area", typeof(IEnumerable<AdministratorResource>))]

        [HttpGet("area/{areaName}")]
        [ProducesResponseType(typeof(IEnumerable<AdministratorResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<AdministratorResource>> GetAllByAreaAsync(string areaName)
        {
            var admins = await _administratorService.GetAllByAreaAsync(areaName);
            var resources = _mapper.Map<IEnumerable<Administrator>, IEnumerable<AdministratorResource>>(admins);
            return resources;
        }

        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Admin By Id",
            Description = "Get a Admin By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Admin By Id", typeof(AdministratorResource))]

        [HttpGet("{adminId}")]
        [ProducesResponseType(typeof(AdministratorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int adminId)
        {
            var result = await _administratorService.GetByIdAsync(adminId);

            if (!result.Success)
                return BadRequest(result.Message);

            var administratorResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);
            return Ok(administratorResource);
        }

        /******************************************/
                        /*SAVE ADMIN*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save Admin",
            Description = "Create a Admin",
            OperationId = "SaveAdmin")]
        [SwaggerResponse(200, "Admin Created", typeof(AdministratorResource))]

        [HttpPost]
        [ProducesResponseType(typeof(AdministratorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveAdministratorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var admin = _mapper.Map<SaveAdministratorResource, Administrator>(resource);
            var result = await _administratorService.SaveAsync(admin);

            if (!result.Success)
                return BadRequest(result.Message);

            var adminResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);
            return Ok(adminResource);
        }

        /******************************************/
                    /*UPDATE ADMIN*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Update Admin",
           Description = "Update a Admin",
           OperationId = "UpdateAdmin")]
        [SwaggerResponse(200, "Admin Updated", typeof(AdministratorResource))]

        [HttpPut("{adminId}")]
        [ProducesResponseType(typeof(AdministratorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int adminId, [FromBody] SaveAdministratorResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var admin = _mapper.Map<SaveAdministratorResource, Administrator>(resource);
            var result = await _administratorService.UpdateAsync(adminId, admin);

            if (!result.Success)
                return BadRequest(result.Message);

            var adminResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);
            return Ok(adminResource);
        }


        /******************************************/
                    /*DELETE ADMIN*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete Admin",
           Description = "Delete a Admin",
           OperationId = "DeleteAdmin")]
        [SwaggerResponse(200, "Admin Deleted", typeof(AdministratorResource))]

        [HttpDelete("{adminId}")]
        [ProducesResponseType(typeof(AdministratorResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int adminId)
        {
            var result = await _administratorService.DeleteAsync(adminId);

            if (!result.Success)
                return BadRequest(result.Message);

            var adminResource = _mapper.Map<Administrator, AdministratorResource>(result.Resource);
            return Ok(adminResource);
        }

    }
}
