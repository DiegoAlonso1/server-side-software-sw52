using Google.Apis.Drive.v3.Data;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class DriveFileResponse : BaseResponse<DriveFileResource>
    {
        public DriveFileResponse(DriveFileResource resource) : base(resource)
        {
        }

        public DriveFileResponse(string message) : base(message)
        {
        }

        public DriveFileResponse() : base()
        {

        }
    }
}
