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

        [HttpGet("members/memberId")]
        [ProducesResponseType(typeof(TrelloMemberResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloMemberByIdAsync(string memberId)
        {
            var result = await _trelloService.GetMemberByIdAsync(memberId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*GET TRELLO MEMBERS BY CARD ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Members By Card Id",
            Description = "Get all Trello Members By Card Id",
            OperationId = "GetAllTrelloMembersByCardId")]
        [SwaggerResponse(200, "Trello Members By Card Id", typeof(IEnumerable<TrelloMemberResource>))]

        [HttpGet("cards/{cardId}/members")]
        [ProducesResponseType(typeof(IEnumerable<TrelloMemberResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<TrelloMemberResource>> GetAllMembersByCardIdAsync(string cardId)
        {
            var members = await _trelloService.GetAllMembersByCardIdAsync(cardId);
            return members;
        }



        /******************************************/
        /*GET TRELLO BOARDS BY MEMBER ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Boards By Member Id",
            Description = "Get all Trello Boards By Member Id",
            OperationId = "GetAllTrelloBoardsByMemberId")]
        [SwaggerResponse(200, "Trello Boards By Member Id", typeof(IEnumerable<TrelloBoardResource>))]

        [HttpGet("members/{memberId}/boards")]
        [ProducesResponseType(typeof(IEnumerable<TrelloBoardResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId)
        {
            var boards = await _trelloService.GetAllBoardsByMemberIdAsync(memberId);
            return boards;
        }


        /******************************************/
        /*GET TRELLO BOARD BY ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Board By Id",
            Description = "Get a Trello Board By Id",
            OperationId = "GetATrelloBoardById")]
        [SwaggerResponse(200, "Trello Board By Id", typeof(TrelloBoardResource))]

        [HttpGet("boards/{boardId}")]
        [ProducesResponseType(typeof(TrelloBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloBoardByIdAsync(string boardId)
        {
            var result = await _trelloService.GetBoardByIdAsync(boardId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*POST TRELLO BOARD ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Post Trello Board",
            Description = "Post a Trello Board",
            OperationId = "PostTrelloBoard")]
        [SwaggerResponse(200, "Trello Board Created", typeof(TrelloBoardResource))]

        [HttpPost("boards")]
        [ProducesResponseType(typeof(TrelloBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> SaveTrelloBoardAsync([FromBody] SaveTrelloBoardResource resource)
        {
            var result = await _trelloService.SaveBoardAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }


        /******************************************/
        /*PUT TRELLO BOARD ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Put Trello Board",
            Description = "Put a Trello Board",
            OperationId = "PutTrelloBoard")]
        [SwaggerResponse(200, "Trello Board Updated", typeof(TrelloBoardResource))]

        [HttpPut("boards/{boardId}")]
        [ProducesResponseType(typeof(TrelloBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateTrelloBoardAsync(string boardId, [FromBody] SaveTrelloBoardResource resource)
        {
            var result = await _trelloService.UpdateBoardAsync(boardId, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }


        /******************************************/
        /*DELETE TRELLO BOARD ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Delete Trello Board",
            Description = "Delete a Trello Board",
            OperationId = "DeleteTrelloBoard")]
        [SwaggerResponse(200, "Trello Board Deleted", typeof(TrelloBoardResource))]

        [HttpDelete("boards/{boardId}")]
        [ProducesResponseType(typeof(TrelloBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteTrelloBoardAsync(string boardId)
        {
            var result = await _trelloService.DeleteBoardAsync(boardId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        /******************************************/
        /*GET TRELLO CARDS BY BOARD ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Cards By Board Id",
            Description = "Get all Trello Cards By Board Id",
            OperationId = "GetAllTrelloCardsByBoardId")]
        [SwaggerResponse(200, "Trello Cards By Board Id", typeof(IEnumerable<TrelloCardResource>))]

        [HttpGet("boards/{boardId}/cards")]
        [ProducesResponseType(typeof(IEnumerable<TrelloCardResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<TrelloCardResource>> GetAllCardsByBoardIdAsync(string boardId)
        {
            var cards = await _trelloService.GetAllCardsByBoardIdAsync(boardId);
            return cards;
        }



        /******************************************/
        /*GET TRELLO CARD BY ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Card By Id",
            Description = "Get a Trello Card By Id",
            OperationId = "GetATrelloCardById")]
        [SwaggerResponse(200, "Trello Card By Id", typeof(TrelloCardResource))]

        [HttpGet("cards/{cardId}")]
        [ProducesResponseType(typeof(TrelloCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloCardByIdAsync(string cardId)
        {
            var result = await _trelloService.GetCardByIdAsync(cardId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        ///******************************************/
        ///*POST TRELLO CARD ASYNC*/
        ///******************************************/

        //[SwaggerOperation(
        //    Summary = "Post Trello Card",
        //    Description = "Post a Trello Card",
        //    OperationId = "PostTrelloCard")]
        //[SwaggerResponse(200, "Trello Card Created", typeof(TrelloCardResource))]

        //[HttpPost("cards")]
        //[ProducesResponseType(typeof(TrelloCardResource), 200)]
        //[ProducesResponseType(typeof(BadRequestResult), 404)]
        //public async Task<IActionResult> SaveTrelloCardAsync([FromBody] SaveTrelloCardResource resource)
        //{
        //    var result = await _trelloService.SaveCardAsync(resource);

        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Resource);
        //}


        /******************************************/
        /*PUT TRELLO CARD ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Put Trello Card",
            Description = "Put a Trello Card",
            OperationId = "PutTrelloCard")]
        [SwaggerResponse(200, "Trello Card Updated", typeof(TrelloCardResource))]

        [HttpPut("cards/{cardId}")]
        [ProducesResponseType(typeof(TrelloCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateTrelloCardAsync(string cardId, [FromBody] SaveTrelloCardResource resource)
        {
            var result = await _trelloService.UpdateCardAsync(cardId, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }


        /******************************************/
        /*DELETE TRELLO CARD ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Delete Trello Card",
            Description = "Delete a Trello Card",
            OperationId = "DeleteTrelloCard")]
        [SwaggerResponse(200, "Trello Card Deleted", typeof(TrelloCardResource))]

        [HttpDelete("cards/{cardId}")]
        [ProducesResponseType(typeof(TrelloCardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteTrelloCardAsync(string cardId)
        {
            var result = await _trelloService.DeleteCardAsync(cardId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*GET TRELLO LISTS BY BOARD ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Lists By Board Id",
            Description = "Get all Trello Lists By Board Id",
            OperationId = "GetAllTrelloListsByBoardId")]
        [SwaggerResponse(200, "Trello Lists By Board Id", typeof(IEnumerable<TrelloListResource>))]

        [HttpGet("boards/{boardId}/lists")]
        [ProducesResponseType(typeof(IEnumerable<TrelloListResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<TrelloListResource>> GetAllListsByBoardIdAsync(string boardId)
        {
            var lists = await _trelloService.GetAllListsByBoardIdAsync(boardId);
            return lists;
        }


        /******************************************/
        /*GET TRELLO LISTS BY CARD ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello List By Card Id",
            Description = "Get a Trello List By Card Id",
            OperationId = "GetATrelloListByCardId")]
        [SwaggerResponse(200, "Trello List By Card Id", typeof(TrelloListResource))]

        [HttpGet("cards/{cardId}/list")]
        [ProducesResponseType(typeof(TrelloListResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloListByCardIdAsync(string cardId)
        {
            var result = await _trelloService.GetListByCardIdAsync(cardId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*GET TRELLO LISTS BY ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello List By Id",
            Description = "Get a Trello List By Id",
            OperationId = "GetATrelloListById")]
        [SwaggerResponse(200, "Trello List By Id", typeof(TrelloListResource))]

        [HttpGet("lists/{listId}")]
        [ProducesResponseType(typeof(TrelloListResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloListByIdAsync(string listId)
        {
            var result = await _trelloService.GetListByIdAsync(listId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }




        /******************************************/
        /*POST TRELLO LIST ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Post Trello List",
            Description = "Post a Trello List",
            OperationId = "PostTrelloList")]
        [SwaggerResponse(200, "Trello List Created", typeof(TrelloListResource))]

        [HttpPost("boards/{boardId}/lists")]
        [ProducesResponseType(typeof(TrelloListResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> SaveTrelloListOnABoardAsync([FromBody] SaveTrelloListResource resource, string boardId)
        {
            var result = await _trelloService.SaveListOnABoardAsync(resource, boardId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*PUT TRELLO LIST ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Put Trello List",
            Description = "Put a Trello List",
            OperationId = "PutTrelloList")]
        [SwaggerResponse(200, "Trello List Updated", typeof(TrelloListResource))]

        [HttpPut("lists/{listId}")]
        [ProducesResponseType(typeof(TrelloListResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateTrelloListAsync(string listId, [FromBody] SaveTrelloListResource resource)
        {
            var result = await _trelloService.UpdateListOnABoardAsync(listId, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*GET TRELLO ORGANIZATIONS BY MEMBER ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Organizations By Member Id",
            Description = "Get all Trello Organizations By Member Id",
            OperationId = "GetAllTrelloOrganizationsByMemberId")]
        [SwaggerResponse(200, "Trello Organizations By Member Id", typeof(IEnumerable<TrelloOrganizationResource>))]

        [HttpGet("members/{memberId}/organizations")]
        [ProducesResponseType(typeof(IEnumerable<TrelloOrganizationResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<TrelloOrganizationResource>> GetAllOrganizationsByMemberIdAsync(string memberId)
        {
            var organizations = await _trelloService.GetAllOrganizationsByMemberIdAsync(memberId);
            return organizations;
        }



        /******************************************/
        /*POST TRELLO ORGANIZATION ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Post Trello Organization",
            Description = "Post a Trello Organization",
            OperationId = "PostTrelloOrganization")]
        [SwaggerResponse(200, "Trello Organization Created", typeof(TrelloOrganizationResource))]

        [HttpPost("organizations")]
        [ProducesResponseType(typeof(TrelloOrganizationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> SaveTrelloOrganizationAsync([FromBody] SaveTrelloOrganizationResource resource)
        {
            var result = await _trelloService.SaveOrganizationAsync(resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*PUT TRELLO ORGANIZATION ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Put Trello Organization",
            Description = "Put a Trello Organization",
            OperationId = "PutTrelloOrganization")]
        [SwaggerResponse(200, "Trello Organization Updated", typeof(TrelloOrganizationResource))]

        [HttpPut("organizations/{organizationId}")]
        [ProducesResponseType(typeof(TrelloOrganizationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateTrelloOrganizationAsync(string organizationId, [FromBody] SaveTrelloOrganizationResource resource)
        {
            var result = await _trelloService.UpdateOrganizationAsync(organizationId, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
        /*DELETE TRELLO ORGANIZATION ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Delete Trello Organization",
            Description = "Delete a Trello Organization",
            OperationId = "DeleteTrelloOrganization")]
        [SwaggerResponse(200, "Trello Organization Deleted", typeof(TrelloOrganizationResource))]

        [HttpDelete("organizations/{organizationId}")]
        [ProducesResponseType(typeof(TrelloOrganizationResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteTrelloOrganizationAsync(string organizationId)
        {
            var result = await _trelloService.DeleteOrganizationAsync(organizationId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }
    }
}
