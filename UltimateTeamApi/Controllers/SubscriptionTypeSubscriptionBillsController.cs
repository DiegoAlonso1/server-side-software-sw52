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
    [Route("api/sub-types/{subTypeId}/subscriptionBills")]
    [Produces("application/json")]
    [ApiController]
    public class SubscriptionTypeSubscriptionBillsController : ControllerBase
    {
        private readonly ISubscriptionBillService _subscriptionBillService;
        private readonly IMapper _mapper;

        public SubscriptionTypeSubscriptionBillsController(ISubscriptionBillService subscriptionBillService, IMapper mapper)
        {
            _subscriptionBillService = subscriptionBillService;
            _mapper = mapper;
        }

        /******************************************/
            /*GET ALL SUB-BILLS BY TYPE ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Sub Bills By Type Id",
            Description = "Get List of Sub Bills By Type Id",
            OperationId = "GetAllSubBillsByTypeId")]
        [SwaggerResponse(200, "List of Sub Bills By Type Id", typeof(IEnumerable<SubscriptionBillResource>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<SubscriptionBillResource>> GetAllByAdministratorIdAsync(int subTypeId)
        {
            var subscriptionBills = await _subscriptionBillService.GetAllBySubscriptionTypeIdAsync(subTypeId);
            var resources = _mapper.Map<IEnumerable<SubscriptionBill>, IEnumerable<SubscriptionBillResource>>(subscriptionBills);
            return resources;
        }
    }
}
