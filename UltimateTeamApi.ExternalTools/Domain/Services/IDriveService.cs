using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface IDriveService
    {
        void AssignGoogleCredential(IGoogleAuthProvider auth, GoogleCredential credencial);
        Task<IEnumerable<File>> GetAllDriveFiles();
        Task<DriveFileResponse> UploadFile();
    }
}
