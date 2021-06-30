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
        private readonly IMapper _mapper;

        public PayPalSubscriptionsController(IPayPalService payPalService, IMapper mapper)
        {
            _payPalService = payPalService;
            _mapper = mapper;
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
        public async Task<IActionResult> GetByIdAsync([FromBody] SaveSuscription resource)
        {
            var result = await _payPalService.SuscribeToAPlan(resource);

            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Resource);
        }
    }
}
