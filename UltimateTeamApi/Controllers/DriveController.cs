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
using UltimateTeamApi.ExternalTools.Converters;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [SwaggerTag("To use the following endpoints you must first use the Login link outside the swagger (in your browser) to give Google permissions. Then you can return to this page and make use of the endpoints.")]
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
        public async Task<IActionResult> LoginDriveAccountAsync([FromServices] IGoogleAuthProvider auth)
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
        public async Task<IActionResult> LogoutDriveAccountAsync()
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
               /*GET DRIVE FILE BY ID ASYNC*/
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
           /*GET ALL DRIVE FILES BY NAME ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Drive Files By Name",
            Description = "Get List of Drive Files By Name",
            OperationId = "GetAllDriveFilesByName")]
        [SwaggerResponse(200, "List of Drive Files", typeof(IEnumerable<DriveFileResource>))]

        [HttpGet("files/name/{fileName}")]
        [ProducesResponseType(typeof(IEnumerable<DriveFileResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IEnumerable<DriveFileResource>> GetAllDriveFilesByNameAsync([FromServices] IGoogleAuthProvider auth, string fileName)
        {
            var result = await _driveService.GetAllDriveFilesByNameAsync(auth, fileName);
            return result;
        }



        /******************************************/
                /*SAVE DRIVE FILE ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Save Drive File",
            Description = "Save a Drive File",
            OperationId = "SaveDriveFile")]
        [SwaggerResponse(200, "Drive File Saved", typeof(DriveFileResource))]

        [HttpPost("files")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(DriveFileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IActionResult> SaveDriveFileAsync([FromServices] IGoogleAuthProvider auth, IFormFile file)
        {
            var result = await _driveService.SaveDriveFileAsync(auth, file);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
                /*UPDATE DRIVE FILE ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Update Drive File",
            Description = "Update a Drive File",
            OperationId = "UpdateDriveFile")]
        [SwaggerResponse(200, "Drive File Updated", typeof(DriveFileResource))]

        [HttpPut("files/{fileId}")]
        [ProducesResponseType(typeof(DriveFileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IActionResult> UpdateDriveFileAsync([FromServices] IGoogleAuthProvider auth, string fileId, [FromBody]SaveDriveFileResource resource)
        {
            var result = await _driveService.UpdateDriveFileAsync(auth, fileId, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }



        /******************************************/
                /*DELETE DRIVE FILE ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Delete Drive File",
            Description = "Delete a Drive File",
            OperationId = "DeleteDriveFile")]
        [SwaggerResponse(200, "Drive File Deleted", typeof(DriveFileResource))]

        [HttpDelete("files/{fileId}")]
        [ProducesResponseType(typeof(DriveFileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.Drive)]
        public async Task<IActionResult> DeleteDriveFileAsync([FromServices] IGoogleAuthProvider auth, string fileId)
        {
            var result = await _driveService.DeleteDriveFileAsync(auth, fileId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Resource);
        }
    }
}
