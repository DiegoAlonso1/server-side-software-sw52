using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class DriveService : IDriveService
    {
        private Google.Apis.Drive.v3.DriveService driveService;

        public void AssignGoogleCredential(IGoogleAuthProvider auth, GoogleCredential credencial)
        {
            driveService = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credencial
            });
        }

        public async Task<IEnumerable<File>> GetAllDriveFiles()
        {
            var result = await driveService.Files.List().ExecuteAsync();
            return result.Files;
        }

        public async Task<DriveFileResponse> UploadFile()
        {
            try
            {
                //TODO: Complete this part https://developers.google.com/api-client-library/dotnet/get_started
                //driveService.Files.Create(
                //    new File { Name = "FileName" },
                //    uploadStream,
                //    "image/jpeg"
                //);
                return new DriveFileResponse(new File());
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while uploading a file: {ex.Message}");
            }
        }
    }
}
