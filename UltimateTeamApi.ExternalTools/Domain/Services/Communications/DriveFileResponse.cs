using Google.Apis.Drive.v3.Data;

namespace UltimateTeamApi.ExternalTools.Domain.Services.Communications
{
    public class DriveFileResponse : BaseResponse<File>
    {
        public DriveFileResponse(File resource) : base(resource)
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
