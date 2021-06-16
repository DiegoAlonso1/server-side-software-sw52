using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Resources;

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
        /*ASSIGN DRIVE CREDENTIAL*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Assign Drive Credential",
            Description = "Assign Drive Credential",
            OperationId = "AssignDriveCredential")]
        [SwaggerResponse(200, "Drive Credential Assigned", typeof(IActionResult))]

        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.DriveReadonly)]
        public async Task<IActionResult> AssignGoogleCredential([FromServices] IGoogleAuthProvider auth)
        {
            var result = await _driveService.AssignGoogleCredential(auth);

            if (!result.Success)
                return BadRequest();

            return Ok();
        }



        /******************************************/
        /*GET ALL DRIVE FILES ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Drive Files",
            Description = "Get List of All Drive Files",
            OperationId = "GetAllDriveFiles")]
        [SwaggerResponse(200, "List of Drive Files", typeof(IEnumerable<Google.Apis.Drive.v3.Data.File>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DriveFileResource>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.DriveReadonly)]
        public async Task<IEnumerable<DriveFileResource>> GetAllDriveFilesAsync()
        {
            var result = await _driveService.GetAllDriveFiles();
            
            return null;
        }
    }
}
