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
    public class SubscriptionTypesController : ControllerBase
    {
        private readonly ISubscriptionTypeService _subscriptionTypeService;
        private readonly IMapper _mapper;

        public SubscriptionTypesController(ISubscriptionTypeService subscriptionTypeService, IMapper mapper)
        {
            _subscriptionTypeService = subscriptionTypeService;
            _mapper = mapper;
        }

        /******************************************/
                    /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Subscription Types",
            Description = "Get List of All Subscription Types",
            OperationId = "GetAllSubscriptionTypes")]
        [SwaggerResponse(200, "List of Subscription Types", typeof(IEnumerable<SubscriptionTypeResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionTypeResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SubscriptionTypeResource>> GetAllAsync()
        {
            var subscriptionTypes = await _subscriptionTypeService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<SubscriptionType>, IEnumerable<SubscriptionTypeResource>>(subscriptionTypes);
            return resources;
        }

        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Subscription Type By Id",
            Description = "Get a Subscription Type By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Subscription Type By Id", typeof(SubscriptionTypeResource))]

        [HttpGet("{subId}")]
        [ProducesResponseType(typeof(SubscriptionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int subId)
        {
            var result = await _subscriptionTypeService.GetByIdAsync(subId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionTypeResource = _mapper.Map<SubscriptionType, SubscriptionTypeResource>(result.Resource);
            return Ok(subscriptionTypeResource);
        }

        /******************************************/
                /*SAVE SUBSCRIPTION TYPE*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save Subscription Type",
            Description = "Create a Subscription Type",
            OperationId = "SaveSubscriptionType")]
        [SwaggerResponse(200, "Subscription Type Created", typeof(SubscriptionTypeResource))]

        [HttpPost]
        [ProducesResponseType(typeof(SubscriptionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriptionTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriptionBill = _mapper.Map<SaveSubscriptionTypeResource, SubscriptionType>(resource);
            var result = await _subscriptionTypeService.SaveAsync(subscriptionBill);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionTypeResource = _mapper.Map<SubscriptionType, SubscriptionTypeResource>(result.Resource);
            return Ok(subscriptionTypeResource);
        }

        /******************************************/
                /*UPDATE SUBSCRIPTION TYPE*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Update Subscription Type",
           Description = "Update a Subscription Type",
           OperationId = "UpdateSubscriptionType")]
        [SwaggerResponse(200, "Subscription Type Updated", typeof(SubscriptionTypeResource))]

        [HttpPut("{subId}")]
        [ProducesResponseType(typeof(SubscriptionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int subId, [FromBody] SaveSubscriptionTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriptionBill = _mapper.Map<SaveSubscriptionTypeResource, SubscriptionType>(resource);
            var result = await _subscriptionTypeService.UpdateAsync(subId, subscriptionBill);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionTypeResource = _mapper.Map<SubscriptionType, SubscriptionTypeResource>(result.Resource);
            return Ok(subscriptionTypeResource);
        }


        /******************************************/
                /*DELETE SUBSCRIPTION TYPE*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete Subscription Type",
           Description = "Delete a Subscription Type",
           OperationId = "DeleteSubscriptionType")]
        [SwaggerResponse(200, "Subscription Type Deleted", typeof(SubscriptionTypeResource))]

        [HttpDelete("{subId}")]
        [ProducesResponseType(typeof(SubscriptionTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int subId)
        {
            var result = await _subscriptionTypeService.DeleteAsync(subId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionTypeResource = _mapper.Map<SubscriptionType, SubscriptionTypeResource>(result.Resource);
            return Ok(subscriptionTypeResource);
        }
    }
}
