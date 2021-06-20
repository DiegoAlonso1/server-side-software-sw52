using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Resources;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DriveController : ControllerBase
    {
        private readonly IDriveService _driveService;

        public DriveController(IDriveService driveService)
        {
            _driveService = driveService;
        }



        /******************************************/
                /*LOGIN DRIVE ACCOUNT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Login Drive Account",
            Description = "Login Drive Account",
            OperationId = "LoginDriveAccount")]
        [SwaggerResponse(200, "Drive Account Logged", typeof(string))]

        [HttpGet("login")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IActionResult> LoginDriveAsyncAccount([FromServices] IGoogleAuthProvider auth)
        {
            var result = await _driveService.AssignGoogleCredentialAsync(auth);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("Logged");
        }



        /******************************************/
                /*LOGOUT DRIVE ACCOUNT*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Logout Drive Account",
            Description = "Logout Drive Account",
            OperationId = "LogoutDriveAccount")]
        [SwaggerResponse(200, "Drive Account Logged out", typeof(string))]

        [HttpGet("logout")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> LogoutDriveAsyncAccount()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("Logged out");
            }

            return Ok("You are already logged out");
        }



        /******************************************/
                /*GET ALL DRIVE FILES ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Drive Files",
            Description = "Get List of All Drive Files",
            OperationId = "GetAllDriveFiles")]
        [SwaggerResponse(200, "List of Drive Files", typeof(IEnumerable<DriveFileResource>))]

        [HttpGet("files")]
        [ProducesResponseType(typeof(IEnumerable<DriveFileResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IEnumerable<DriveFileResource>> GetAllDriveFilesAsync([FromServices] IGoogleAuthProvider auth)
        {
            var result = await _driveService.GetAllDriveFilesAsync(auth);
            return result;
        }



        /******************************************/
                /*GET ALL DRIVE FILES ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get Drive File By Id",
            Description = "Get a Drive File By Id",
            OperationId = "GetDriveFileById")]
        [SwaggerResponse(200, "Get Drive File By Id", typeof(DriveFileResource))]

        [HttpGet("files/{fileId}")]
        [ProducesResponseType(typeof(DriveFileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IActionResult> GetDriveFileByIdAsync([FromServices] IGoogleAuthProvider auth, string fileId)
        {
            var result = await _driveService.GetDriveFileByIdAsync(auth, fileId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
                /*UPLOAD DRIVE FILES ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Upload Drive File",
            Description = "Upload a Drive File",
            OperationId = "UploadDriveFile")]
        [SwaggerResponse(200, "Drive File Uploaded", typeof(string))]

        [HttpPost("file")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IActionResult> UploadDriveFileAsync([FromServices] IGoogleAuthProvider auth, [FromBody] SaveDriveFileResource resource)
        {
            var result = await _driveService.UploadFileAsync(auth, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok("File Uploaded");
        }
    }
}
