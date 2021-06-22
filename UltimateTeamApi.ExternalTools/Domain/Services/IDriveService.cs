using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;

namespace UltimateTeamApi.ExternalTools.Domain.Services
{
    public interface IDriveService
    {
        Task<IEnumerable<DriveFileResource>> GetAllDriveFilesAsync(IGoogleAuthProvider auth);
        Task<DriveFileResponse> GetDriveFileByIdAsync(IGoogleAuthProvider auth, string fileId);
        Task<DriveFileResponse> AssignGoogleCredentialAsync(IGoogleAuthProvider auth);
        Task<DriveFileResponse> UploadFileAsync(IGoogleAuthProvider auth, SaveDriveFileResource resource);
        Task<DriveFileResponse> CreateCarpet(IGoogleAuthProvider auth/*, SaveDriveCarpetResource resource*/);
    }
}
