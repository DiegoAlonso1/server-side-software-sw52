using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface IDriveService
    {
        Task<IEnumerable<File>> GetAllDriveFiles();          
        Task<DriveFileResponse> AssignGoogleCredential(IGoogleAuthProvider auth);
        Task<DriveFileResponse> UploadFile();
    }
}
