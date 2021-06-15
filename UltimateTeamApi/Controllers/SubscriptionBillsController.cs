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
    public class SubscriptionBillsController : ControllerBase
    {
        private readonly ISubscriptionBillService _subscriptionBillService;
        private readonly IMapper _mapper;

        public SubscriptionBillsController(ISubscriptionBillService subscriptionBillService, IMapper mapper)
        {
            _subscriptionBillService = subscriptionBillService;
            _mapper = mapper;
        }

        /******************************************/
                    /*GET ALL ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Subscription Bills",
            Description = "Get List of All Subscription Bills",
            OperationId = "GetAllSubscriptionBills")]
        [SwaggerResponse(200, "List of Subscription Bills", typeof(IEnumerable<SubscriptionBillResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubscriptionBillResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SubscriptionBillResource>> GetAllAsync()
        {
            var subscriptionBills = await _subscriptionBillService.GetAllAsync();
            var resources = _mapper.Map<IEnumerable<SubscriptionBill>, IEnumerable<SubscriptionBillResource>>(subscriptionBills);
            return resources;
        }

        /******************************************/
                    /*GET BY ID ASYNC*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Get Subscription Bill By Id",
            Description = "Get a Subscription Bill By Id",
            OperationId = "GetById")]
        [SwaggerResponse(200, "Subscription Bill By Id", typeof(SubscriptionBillResource))]

        [HttpGet("{subId}")]
        [ProducesResponseType(typeof(SubscriptionBillResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetByIdAsync(int subId)
        {
            var result = await _subscriptionBillService.GetByIdAsync(subId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionBillResource = _mapper.Map<SubscriptionBill, SubscriptionBillResource>(result.Resource);
            return Ok(subscriptionBillResource);
        }

        /******************************************/
                    /*SAVE SUBSCRIPTION BILL*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Save Subscription Bill",
            Description = "Create a Subscription Bill",
            OperationId = "SaveSubscriptionBill")]
        [SwaggerResponse(200, "Subscription Bill Created", typeof(SubscriptionBillResource))]

        [HttpPost]
        [ProducesResponseType(typeof(SubscriptionBillResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSubscriptionBillResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriptionBill = _mapper.Map<SaveSubscriptionBillResource, SubscriptionBill>(resource);
            var result = await _subscriptionBillService.SaveAsync(subscriptionBill);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionBillResource = _mapper.Map<SubscriptionBill, SubscriptionBillResource>(result.Resource);
            return Ok(subscriptionBillResource);
        }

        /******************************************/
                /*UPDATE SUBSCRIPTION BILL*/
        /******************************************/

        [SwaggerOperation(
           Summary = "Update Subscription Bill",
           Description = "Update a Subscription Bill",
           OperationId = "UpdateSubscriptionBill")]
        [SwaggerResponse(200, "Subscription Bill Updated", typeof(SubscriptionBillResource))]

        [HttpPut("{subId}")]
        [ProducesResponseType(typeof(SubscriptionBillResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int subId, [FromBody] SaveSubscriptionBillResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var subscriptionBill = _mapper.Map<SaveSubscriptionBillResource, SubscriptionBill>(resource);
            var result = await _subscriptionBillService.UpdateAsync(subId, subscriptionBill);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionBillResource = _mapper.Map<SubscriptionBill, SubscriptionBillResource>(result.Resource);
            return Ok(subscriptionBillResource);
        }


        /******************************************/
                /*DELETE SUBSCRIPTION BILL*/
        /******************************************/
        [SwaggerOperation(
           Summary = "Delete Subscription Bill",
           Description = "Delete a Subscription Bill",
           OperationId = "DeleteSubscriptionBill")]
        [SwaggerResponse(200, "Subscription Bill Deleted", typeof(SubscriptionBillResource))]

        [HttpDelete("{subId}")]
        [ProducesResponseType(typeof(SubscriptionBillResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int subId)
        {
            var result = await _subscriptionBillService.DeleteAsync(subId);

            if (!result.Success)
                return BadRequest(result.Message);

            var subscriptionBillResource = _mapper.Map<SubscriptionBill, SubscriptionBillResource>(result.Resource);
            return Ok(subscriptionBillResource);
        }
    }
}
