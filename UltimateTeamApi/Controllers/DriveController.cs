using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;

namespace UltimateTeamApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DriveController
    {
        private readonly IDriveService _driveService;

        public DriveController(IDriveService driveService)
        {
            _driveService = driveService;
        }



        /******************************************/
            /*GET ALL DRIVE FILES ASYNC*/
        /******************************************/

        [SwaggerOperation(
            Summary = "Get All Drive Files",
            Description = "Get List of All Drive Files",
            OperationId = "GetAllDriveFiles")]
        [SwaggerResponse(200, "List of Drive Files", typeof(IEnumerable<string>))]

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        [GoogleScopedAuthorize(DriveService.ScopeConstants.DriveReadonly)]
        public async Task<IEnumerable<string>> GetAllDriveFilesAsync([FromServices]IGoogleAuthProvider auth)
        {
            var result = _driveService.GetAllDriveFiles();

            return null;
        }
    }
}
