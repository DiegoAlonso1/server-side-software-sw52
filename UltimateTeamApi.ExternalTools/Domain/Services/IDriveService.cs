using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface IDriveService
    {
        Task<IEnumerable<string>> GetAllDriveFiles(IGoogleAuthProvider auth);          
        Task<DriveFileResponse> AssignGoogleCredential(IGoogleAuthProvider auth);
        Task<DriveFileResponse> UploadFile(IGoogleAuthProvider auth, string filePath, string fileName, string fileExtension);
    }
}
