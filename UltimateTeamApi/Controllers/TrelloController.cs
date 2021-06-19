using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        /*LOGIN TRELLO ACCOUNT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Login Trello Account",
            Description = "Login Trello Account",
            OperationId = "LoginTrelloAccount")]
        [SwaggerResponse(200, "Trello Account Logged", typeof(string))]

        [HttpGet("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> LoginTrelloAccount()
        {
            var result = await _trelloService.AssignToken();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Logged");
        }


        /******************************************/
        /*LOGOUT TRELLO ACCOUNT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Logout Trello Account",
            Description = "Logout Trello Account",
            OperationId = "LogoutTrelloAccount")]
        [SwaggerResponse(200, "Trello Account Logged out", typeof(string))]

        [HttpGet("logout")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> LogoutTrelloAccount()
        {
            var result = await _trelloService.UnassignToken();

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Logged out");
        }



        /******************************************/
        /*GET TRELLO MEMBER BY ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Member By Id",
            Description = "Get a Trello Member By Id",
            OperationId = "GetATrelloMemberById")]
        [SwaggerResponse(200, "Trello Member By Id", typeof(TrelloMemberResource))]

        [HttpGet]
        [ProducesResponseType(typeof(TrelloMemberResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloMemberByIdAsync(string memberId)
        {
            var result = await _trelloService.GetMemberById(memberId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        ///******************************************/
        ///*GET TRELLO BOARDS BY MEMBER ID ASYNC*/
        ///******************************************/

        //[SwaggerOperation(
        //    Summary = "Get Trello Boards By Member Id",
        //    Description = "Get all Trello Boards By Member Id",
        //    OperationId = "GetAllTrelloBoardsByMemberId")]
        //[SwaggerResponse(200, "Trello Boards By Member Id", typeof(TrelloBoardResource))]

        //[HttpGet]
        //[ProducesResponseType(typeof(TrelloBoardResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> GetAllBoardsByMemberIdAsync(string memberId)
        //{
        //    var result = await _trelloService.GetAllBoardsByMemberId(memberId);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Resource);
        //}

        //60cd2ed628874b1eb2ba73b2

        ///******************************************/
        ///*GET TRELLO BOARD BY ID ASYNC*/
        ///******************************************/

        //[SwaggerOperation(
        //    Summary = "Get Trello Board By Id",
        //    Description = "Get a Trello Board By Id",
        //    OperationId = "GetATrelloBoardById")]
        //[SwaggerResponse(200, "Trello Board By Id", typeof(TrelloBoardResource))]

        //[HttpGet]
        //[ProducesResponseType(typeof(TrelloBoardResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> GetTrelloBoardByIdAsync(string boardId)
        //{
        //    var result = await _trelloService.GetBoardById(boardId);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Resource);
        //}
    }
}
