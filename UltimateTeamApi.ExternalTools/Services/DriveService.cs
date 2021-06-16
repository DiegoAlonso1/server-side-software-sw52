using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using static Google.Apis.Drive.v3.FilesResource;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class DriveService : IDriveService
    {
        public async Task<DriveFileResponse> AssignGoogleCredential(IGoogleAuthProvider auth)
        {
            Google.Apis.Drive.v3.DriveService driveService = await GetService(auth);

            if (driveService != null)
                return new DriveFileResponse();

            else
                return new DriveFileResponse("An error ocurred while loggin in");
        }

        public async Task<IEnumerable<string>> GetAllDriveFiles(IGoogleAuthProvider auth)
        {
            Google.Apis.Drive.v3.DriveService driveService = await GetService(auth);

            if (driveService == null)
                return new List<string>();

            var result = await driveService.Files.List().ExecuteAsync();
            return result.Files.Select(f => f.Name).ToList();
        }

        public async Task<DriveFileResponse> UploadFile(IGoogleAuthProvider auth, string filePath, string fileName, string fileExtension)
        {
            try
            {
                Google.Apis.Drive.v3.DriveService driveService = await GetService(auth);
                var uploadStream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                CreateMediaUpload createRequest = driveService.Files.Create(
                    new File { Name = fileName },
                    uploadStream,
                    fileExtension//"image/jpeg"
                );

                var uploadTask = createRequest.UploadAsync();
                await uploadTask.ContinueWith(s => s.Dispose());

                return new DriveFileResponse();
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while uploading a file: {ex.Message}");
            }
        }

        private async Task<Google.Apis.Drive.v3.DriveService> GetService(IGoogleAuthProvider auth)
        {
            GoogleCredential credencial = await auth.GetCredentialAsync();

            Google.Apis.Drive.v3.DriveService driveService = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credencial
            });
            return driveService;
        }
    }
}
