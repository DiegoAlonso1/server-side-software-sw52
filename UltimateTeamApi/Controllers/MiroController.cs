using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("For the usage of the following endpoints you have to follow two steps. The First is to log in into your Miro Account and join our team with this url => https://miro.com/welcome/VVFnb3pQaDU4bTdhU2MyZ3h0TGlhV2Vid2NYNTFmZXRWeTVIckpwQXB6Q0ZIdE1lNVVNbFozS2ljb2pBdVMxZnwzMDc0NDU3MzU5NDkwMDgzNTI5 The Second step is to reclaim you access token with this url => https://miro.com/oauth/authorize/?response_type=code&client_id=3074457359489952586&redirect_uri=https://localhost:44345/api/miro/login")]
    public class MiroController : ControllerBase
    {
        private readonly IMiroService _miroService;
        public MiroController(IMiroService miroService)
        {
            _miroService = miroService;
        }

        [SwaggerOperation(
            Summary = "Login Miro Account (Don´t use this endpoint and Use the url to log in)",
            Description = "Login Miro Account",
            OperationId = "LoginMiroAccount")]
        [SwaggerResponse(200, "Miro Account Logged", typeof(MiroAuthenticationResponse))]

        [HttpGet("login")]
        [ProducesResponseType(typeof(MiroAuthenticationResponse), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> LoginMiroAccount([FromQuery] string code, [FromQuery] string client_id, [FromQuery] string team_id)
        {
            var result = await _miroService.GetAuthAsync(code, client_id, team_id);
            return Ok(result);
        }

        /******************************************/
        /*CREATE MIROM BOARD*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Post Miro Board",
            Description = "Post a Miro Board",
            OperationId = "PostMiroBoard")]
        [SwaggerResponse(200, "Miro Board Created", typeof(MiroBoardResource))]

        [HttpPost("boards")]
        [ProducesResponseType(typeof(MiroBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> CreateMiroBoardAsync([FromBody] SaveMiroBoardResource resource, [FromQuery] string accessToken)
        {
            var result = await _miroService.CreateMiroBoardAsync(resource,accessToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        /******************************************/
        /*GET MIRO BOARD*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Miro Board By Id",
            Description = "Get a Miro Board By Id",
            OperationId = "GetMiroBoardById")]
        [SwaggerResponse(200, "Got Miro Board By Id", typeof(MiroBoardResource))]

        [HttpGet("boards/{boardId}")]
        [ProducesResponseType(typeof(MiroBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetMiroBoardByIdAsync(string boardId, [FromQuery]string accessToken)
        {
            var result = await _miroService.GetMiroBoardByIdAsync(boardId,accessToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        /******************************************/
        /*Share MIRO BOARD*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Share Miro Board",
            Description = "Share a Miro Board",
            OperationId = "ShareMiroBoard")]
        [SwaggerResponse(200, "Miro Board Shared", typeof(IEnumerable<MiroBoard4ShareResource>))]

        [HttpPost("boards/{boardId}/share")]
        [ProducesResponseType(typeof(IEnumerable<MiroBoard4ShareResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<MiroBoard4ShareResource>> ShareMiroBoardAsync(string boardId, [FromBody] SaveMiroBoard4ShareResource resource, [FromQuery] string accessToken)
        {
            var result = await _miroService.ShareMiroBoardAsync(boardId, resource,accessToken);

            return result;
        }

        /******************************************/
        /*UPDATE MIRO BOARD*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Update Miro Board",
            Description = "Update a Miro Board",
            OperationId = "UpdateMiroBoard")]
        [SwaggerResponse(200, "Miro Board Updated", typeof(MiroBoardResource))]

        [HttpPut("boards/{boardId}")]
        [ProducesResponseType(typeof(MiroBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> UpdateMiroBoardAsync(string boardId, [FromBody] SaveMiroBoardResource resource, [FromQuery] string accessToken)
        {
            var result = await _miroService.UpdateMiroBoardAsync(boardId,resource,accessToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        /******************************************/
        /*DELETE MIRO BOARD*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Delete Miro Board",
            Description = "Delete a Miro Board",
            OperationId = "Delete MiroBoard")]
        [SwaggerResponse(200, "Miro Board Deleted", typeof(MiroBoardResource))]

        [HttpDelete("boards/{boardId}")]
        [ProducesResponseType(typeof(MiroBoardResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteMiroBoardbyIdAync(string boardId, [FromQuery] string accessToken)
        {
            var result = await _miroService.DeleteMiroBoardbyIdAync(boardId,accessToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        /******************************************/
        /*GET USER BY ID*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Miro User By Id",
            Description = "Get a Miro User By Id",
            OperationId = "GetMiroUserById")]
        [SwaggerResponse(200, "Got Miro User By Id", typeof(MiroUserResource))]

        [HttpGet("users/{userdId}")]
        [ProducesResponseType(typeof(MiroUserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetMiroUserByIdAsync(string userdId, [FromQuery] string accessToken)
        {
            var result = await _miroService.GetMiroUserByIdAsync(userdId, accessToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }

        /******************************************/
        /*GET MY USER*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get My User",
            Description = "Get My User",
            OperationId = "GetMiroUserById")]
        [SwaggerResponse(200, "Got My User", typeof(MiroUserResource))]

        [HttpGet("users/me")]
        [ProducesResponseType(typeof(MiroUserResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetMyUserAsync([FromQuery] string accessToken)
        {
            var result = await _miroService.GetMyUserAsync(accessToken);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }
    }
}
