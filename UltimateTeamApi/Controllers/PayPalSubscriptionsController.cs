using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PayPalSubscriptionsController : ControllerBase
    {
        private readonly IPayPalService _payPalService;

        public PayPalSubscriptionsController(IPayPalService payPalService)
        {
            _payPalService = payPalService;

        }


        [SwaggerOperation(
            Summary = "Get Pay Pal Token",
            Description = "Get Pay Pal Token",
            OperationId = "Get Pay Pal Token")]
        [SwaggerResponse(200, "Get Pay Pal Token", typeof(PayPalTokenResource))]

        [HttpGet("login")]
        [ProducesResponseType(typeof(PayPalTokenResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetPayPalTokenAsync()
        {
            var result = await _payPalService.GetTokenAsync();
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result);
        }




        /******************************************/
        /*POST PAY PAL SUBSCRIPTION*/
        /******************************************/
        [SwaggerOperation(
            Summary = "Create a Pay Pal Subscription",
            Description = "Create a Pay Pal Subscription",
            OperationId = "Post Pay Pal Subscription")]
        [SwaggerResponse(200, "Subscription Pay Pal", typeof(PayPalSubscriptionResource))]

        [HttpPost]
        [ProducesResponseType(typeof(PayPalSubscriptionResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostPayPalSubscriptionAsync([FromQuery]string token, [FromBody] SavePaypalSuscriptionResource resource)
        {
            var result = await _payPalService.SuscribePlanAsync(token, resource);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
    }
}
