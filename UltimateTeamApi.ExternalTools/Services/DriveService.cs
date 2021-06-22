using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using UltimateTeamApi.ExternalTools.Converters;
using UltimateTeamApi.ExternalTools.Domain.Services;
using UltimateTeamApi.ExternalTools.Domain.Services.Communications;
using UltimateTeamApi.ExternalTools.Resources;
using static Google.Apis.Drive.v3.FilesResource;

namespace UltimateTeamApi.ExternalTools.Services
{
    public class DriveService : IDriveService
    {
        public async Task<DriveFileResponse> AssignGoogleCredentialAsync(IGoogleAuthProvider auth)
        {
            Google.Apis.Drive.v3.DriveService driveService = await GetServiceAsync(auth);

            if (driveService != null)
                return new DriveFileResponse();

            else
                return new DriveFileResponse("An error ocurred while loggin in");
        }

        public async Task<DriveFileResponse> CreateCarpet(IGoogleAuthProvider auth)
        {
            try
            {
                Google.Apis.Drive.v3.DriveService driveService = await GetServiceAsync(auth);

                //var request = driveService.Files.Create()

                return new DriveFileResponse();
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while uploading a file: {ex.Message}");
            }
        }

        public async Task<IEnumerable<DriveFileResource>> GetAllDriveFilesAsync(IGoogleAuthProvider auth)
        {
            Google.Apis.Drive.v3.DriveService driveService = await GetServiceAsync(auth);

            if (driveService == null)
                return new List<DriveFileResource>();

            var request = driveService.Files.List();
            request.Fields = "files";
            var result = await request.ExecuteAsync();

            IList<DriveFileResource> resources = new List<DriveFileResource>();

            foreach(var file in result.Files)
            {
                var resource = new DriveFileResource
                {
                    Id = file.Id,
                    Name = file.Name,
                    Description = file.Description,
                    CreatedTime = file.CreatedTime,
                    Extension = file.FileExtension,
                    Kind = file.Kind,
                    Owners = new List<DriveUserResource>(),
                    FolderParentsIds = file.Parents,
                    Size = file.Size,
                    Version = file.Version,
                    DownloadLink = file.WebContentLink,
                    ViewLink = file.WebViewLink
                };

                foreach (var user in file.Owners)
                {
                    resource.Owners.Add(new DriveUserResource
                    {
                        Id = user.PermissionId,
                        Name = user.DisplayName,
                        Email = user.EmailAddress,
                        PhotoLink = user.PhotoLink
                    }) ;
                }

                resources.Add(resource);
            }

            return resources;
        }

        public async Task<DriveFileResponse> GetDriveFileByIdAsync(IGoogleAuthProvider auth, string fileId)
        {
            Google.Apis.Drive.v3.DriveService driveService = await GetServiceAsync(auth);

            if (driveService == null)
                return new DriveFileResponse("Error D:");

            var request = driveService.Files.Get(fileId);
            request.Fields = "id,name,description,createdTime,fileExtension,kind,owners,parents,size,version,webContentLink,webViewLink";
            var result = await request.ExecuteAsync();

            DriveFileResource resource = new DriveFileResource
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedTime = result.CreatedTime,
                Extension = result.FileExtension,
                Kind = result.Kind,
                Owners = new List<DriveUserResource>(),
                FolderParentsIds = result.Parents,
                Size = result.Size,
                Version = result.Version,
                DownloadLink = result.WebContentLink,
                ViewLink = result.WebViewLink
            };

            foreach (var user in result.Owners)
            {
                resource.Owners.Add(new DriveUserResource
                {
                    Id = user.PermissionId,
                    Name = user.DisplayName,
                    Email = user.EmailAddress,
                    PhotoLink = user.PhotoLink
                });
            }

            return new DriveFileResponse(resource);
        }

        public async Task<DriveFileResponse> UploadFileAsync(IGoogleAuthProvider auth, SaveDriveFileResource resource)
        {
            try
            {
                Google.Apis.Drive.v3.DriveService driveService = await GetServiceAsync(auth);

                CreateMediaUpload createRequest = driveService.Files.Create(
                    new File { Name = resource.File.Name },
                    resource.File.OpenReadStream(),
                    resource.File.ContentType
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

        private async Task<Google.Apis.Drive.v3.DriveService> GetServiceAsync(IGoogleAuthProvider auth)
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
