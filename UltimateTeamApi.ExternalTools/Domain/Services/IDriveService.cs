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
        Task<IEnumerable<DriveFileResource>> GetAllDriveFilesByNameAsync(IGoogleAuthProvider auth, string fileName);
        Task<DriveFileResponse> GetDriveFileByIdAsync(IGoogleAuthProvider auth, string fileId);
        Task<DriveFileResponse> AssignGoogleCredentialAsync(IGoogleAuthProvider auth);
        Task<DriveFileResponse> SaveDriveFileAsync(IGoogleAuthProvider auth, IFormFile file);
        Task<DriveFileResponse> UpdateDriveFileAsync(IGoogleAuthProvider auth, string fileId, SaveDriveFileResource resource);
        Task<DriveFileResponse> DeleteDriveFileAsync(IGoogleAuthProvider auth, string fileId);
    }
}
