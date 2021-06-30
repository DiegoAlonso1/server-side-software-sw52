using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("To use the following endpoints you must first use the following link outside the swagger (in your browser) to give Trello permissions. Then you can return to this page and make use of the endpoints. Finally you must remove the only # from the url to get the value of its token. LINK => https://api.trello.com/1/authorize?expiration=never&callback_method=fragment&name=UltimateTeam&scope=read,write,account&response_type=token&key=0389b1cfea7b9c070a520f3dfe6f79db&return_url=https://localhost:44345/api/trello/login")]
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
        [SwaggerResponse(200, "Trello Account Logged", typeof(TrelloAuthenticationResponse))]

        [HttpGet("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public IActionResult LoginTrelloAccount([FromQuery]string token)
        {
            var result = _trelloService.AssignToken(token);

            if (result == null)
                return BadRequest("An error ocurred while assigning token. Try again.");

            return Ok(result);
        }



        /******************************************/
            /*GET TRELLO MEMBER BY ID ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Trello Member By Id",
            Description = "Get a Trello Member By Id",
            OperationId = "GetATrelloMemberById")]
        [SwaggerResponse(200, "Trello Member By Id", typeof(TrelloMemberResource))]

        [HttpGet("members/{memberId}")]
        [ProducesResponseType(typeof(TrelloMemberResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetTrelloMemberByIdAsync(string memberId, [FromQuery] string token)
        {
            var result = await _trelloService.GetMemberByIdAsync(memberId, token);

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
        public async Task<IEnumerable<TrelloMemberResource>> GetAllMembersByCardIdAsync(string cardId, [FromQuery]string token)
        {
            var members = await _trelloService.GetAllMembersByCardIdAsync(cardId, token);
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
        public async Task<IEnumerable<TrelloBoardResource>> GetAllBoardsByMemberIdAsync(string memberId, [FromQuery]string token)
        {
            var boards = await _trelloService.GetAllBoardsByMemberIdAsync(memberId, token);
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
        public async Task<IActionResult> GetTrelloBoardByIdAsync(string boardId, [FromQuery]string token)
        {
            var result = await _trelloService.GetBoardByIdAsync(boardId, token);

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
        public async Task<IActionResult> SaveTrelloBoardAsync([FromBody] SaveTrelloBoardResource resource, [FromQuery]string token)
        {
            var result = await _trelloService.SaveBoardAsync(resource, token);

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
        public async Task<IActionResult> UpdateTrelloBoardAsync(string boardId, [FromBody] SaveTrelloBoardResource resource, [FromQuery]string token)
        {
            var result = await _trelloService.UpdateBoardAsync(boardId, resource, token);

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
        public async Task<IActionResult> DeleteTrelloBoardAsync(string boardId, [FromQuery]string token)
        {
            var result = await _trelloService.DeleteBoardAsync(boardId, token);

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
        public async Task<IEnumerable<TrelloCardResource>> GetAllCardsByBoardIdAsync(string boardId, [FromQuery]string token)
        {
            var cards = await _trelloService.GetAllCardsByBoardIdAsync(boardId, token);
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
        public async Task<IActionResult> GetTrelloCardByIdAsync(string cardId, [FromQuery]string token)
        {
            var result = await _trelloService.GetCardByIdAsync(cardId, token);

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
        public async Task<IActionResult> UpdateTrelloCardAsync(string cardId, [FromBody] SaveTrelloCardResource resource, [FromQuery]string token)
        {
            var result = await _trelloService.UpdateCardAsync(cardId, resource, token);

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
        public async Task<IActionResult> DeleteTrelloCardAsync(string cardId, [FromQuery]string token)
        {
            var result = await _trelloService.DeleteCardAsync(cardId, token);

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
        public async Task<IEnumerable<TrelloListResource>> GetAllListsByBoardIdAsync(string boardId, [FromQuery]string token)
        {
            var lists = await _trelloService.GetAllListsByBoardIdAsync(boardId, token);
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
        public async Task<IActionResult> GetTrelloListByCardIdAsync(string cardId, [FromQuery]string token)
        {
            var result = await _trelloService.GetListByCardIdAsync(cardId, token);

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
        public async Task<IActionResult> GetTrelloListByIdAsync(string listId, [FromQuery]string token)
        {
            var result = await _trelloService.GetListByIdAsync(listId, token);

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
        public async Task<IActionResult> SaveTrelloListOnABoardAsync([FromBody] SaveTrelloListResource resource, string boardId, [FromQuery]string token)
        {
            var result = await _trelloService.SaveListOnABoardAsync(resource, boardId, token);

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
        public async Task<IActionResult> UpdateTrelloListAsync(string listId, [FromBody] SaveTrelloListResource resource, [FromQuery]string token)
        {
            var result = await _trelloService.UpdateListOnABoardAsync(listId, resource, token);

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
        public async Task<IEnumerable<TrelloOrganizationResource>> GetAllOrganizationsByMemberIdAsync(string memberId, [FromQuery]string token)
        {
            var organizations = await _trelloService.GetAllOrganizationsByMemberIdAsync(memberId, token);
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
        public async Task<IActionResult> SaveTrelloOrganizationAsync([FromBody] SaveTrelloOrganizationResource resource, [FromQuery]string token)
        {
            var result = await _trelloService.SaveOrganizationAsync(resource, token);

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
        public async Task<IActionResult> UpdateTrelloOrganizationAsync(string organizationId, [FromBody] SaveTrelloOrganizationResource resource, [FromQuery]string token)
        {
            var result = await _trelloService.UpdateOrganizationAsync(organizationId, resource, token);

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
        public async Task<IActionResult> DeleteTrelloOrganizationAsync(string organizationId, [FromQuery]string token)
        {
            var result = await _trelloService.DeleteOrganizationAsync(organizationId, token);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }
    }
}
