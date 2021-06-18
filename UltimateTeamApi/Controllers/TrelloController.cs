using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class TrelloController : ControllerBase
    {
        private readonly ITrelloService _trelloService;
        public TrelloController(ITrelloService trelloService)
        {
            _trelloService = trelloService;
        }

        /******************************************/
        /*GET ALL BOARDS ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Boards",
            Description = "Get List of All Boards",
            OperationId = "GetAllBoards")]
        [SwaggerResponse(200, "List of Boards", typeof(Object))]

        [HttpGet]
        [ProducesResponseType(typeof(Object), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<Object> GetAllBoardsAsync()
        {
            return await _trelloService.GetAllBoards();
        }
    }
}
