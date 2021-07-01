using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
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
            Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

            if (driveService != null)
                return new DriveFileResponse();

            else
                return new DriveFileResponse("An error ocurred while loggin in");
        }

        public async Task<DriveFileResponse> DeleteDriveFileAsync(IGoogleAuthProvider auth, string fileId)
        {
            try
            {
                Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

                var request = driveService.Files.Get(fileId);
                request.Fields = "id,name,description,createdTime,fileExtension,kind,owners,parents,size,version,webContentLink,webViewLink";
                var result = await request.ExecuteAsync();

                DeleteRequest deleteRequest = driveService.Files.Delete(fileId);
                deleteRequest.Fields = "id,name,description,createdTime,fileExtension,kind,owners,parents,size,version,webContentLink,webViewLink";

                var deleteTask = deleteRequest.ExecuteAsync();
                await deleteTask.ContinueWith(s => s.Dispose());

                if (!deleteTask.IsCompletedSuccessfully)
                    throw new Exception("Deleting file request error");

                var resource = createResource(result);
                return new DriveFileResponse(resource);
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while deleting the file: {ex.Message}");
            }
        }

        public async Task<IEnumerable<DriveFileResource>> GetAllDriveFilesAsync(IGoogleAuthProvider auth)
        {
            Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

            if (driveService == null)
                return new List<DriveFileResource>();

            var request = driveService.Files.List();
            request.Fields = "files";
            var result = await request.ExecuteAsync();

            IList<DriveFileResource> resources = new List<DriveFileResource>();

            foreach(var file in result.Files)
            {
                var resource = createResource(file);

                resources.Add(resource);
            }

            return resources;
        }

        public async Task<DriveFileResponse> GetDriveFileByIdAsync(IGoogleAuthProvider auth, string fileId)
        {
            Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

            if (driveService == null)
                return new DriveFileResponse("Error D:");

            try
            {
                var request = driveService.Files.Get(fileId);
                request.Fields = "id,name,description,createdTime,fileExtension,kind,owners,parents,size,version,webContentLink,webViewLink";
                var result = await request.ExecuteAsync();

                var resource = createResource(result);

                return new DriveFileResponse(resource);
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while obtaining the file with Id: {fileId}. {ex.Message}");
            }
        }

        public async Task<IEnumerable<DriveFileResource>> GetAllDriveFilesByNameAsync(IGoogleAuthProvider auth, string fileName)
        {
            Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

            if (driveService == null)
                return new List<DriveFileResource>();

            var request = driveService.Files.List();
            request.Fields = "files";
            var result = await request.ExecuteAsync();

            IList<DriveFileResource> resources = new List<DriveFileResource>();
            var filesWithName = result.Files.Where(f => f.Name == fileName).ToList();

            foreach (var file in filesWithName)
            {
                var resource = createResource(file);

                resources.Add(resource);
            }

            return resources;
        }

        public async Task<DriveFileResponse> SaveDriveFileAsync(IGoogleAuthProvider auth, IFormFile file)
        {
            try
            {
                if (file == null) throw new Exception("The file is empty");

                Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

                CreateMediaUpload createRequest = driveService.Files.Create(
                    new File { Name = file.FileName },
                    file.OpenReadStream(),
                    file.ContentType
                );

                File responseFile = null;

                createRequest.ResponseReceived += ((File file) =>
                {
                    responseFile = file;
                });

                createRequest.Fields = "id,name,description,createdTime,fileExtension,kind,owners,parents,size,version,webContentLink,webViewLink";
                var saveTask = createRequest.UploadAsync();
                await saveTask.ContinueWith(s => s.Dispose());

                while (responseFile == null)
                {
                    System.Threading.Thread.Sleep(500);
                }

                var resource = createResource(responseFile);

                return new DriveFileResponse(resource);
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while saving the file: {ex.Message}");
            }
        }

        public async Task<DriveFileResponse> UpdateDriveFileAsync(IGoogleAuthProvider auth, string fileId, SaveDriveFileResource resource)
        {
            try
            {
                Google.Apis.Drive.v3.DriveService driveService = await getServiceAsync(auth);

                var request = driveService.Files.Get(fileId);
                request.Fields = "name";
                var result = await request.ExecuteAsync();

                result.Name = resource.FileName;

                UpdateRequest updateRequest = driveService.Files.Update(result, fileId);
                updateRequest.Fields = "id,name,description,createdTime,fileExtension,kind,owners,parents,size,version,webContentLink,webViewLink";

                var updateTask = updateRequest.ExecuteAsync();
                await updateTask.ContinueWith(s => s.Dispose());

                var response = updateTask.Result;

                var _resource = createResource(response);

                return new DriveFileResponse(_resource);
            }
            catch (Exception ex)
            {
                return new DriveFileResponse($"An error ocurred while updating the file: {ex.Message}");
            }
        }

        private DriveFileResource createResource(File file)
        {
            DriveFileResource _resource = new DriveFileResource
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
                _resource.Owners.Add(new DriveUserResource
                {
                    Id = user.PermissionId,
                    Name = user.DisplayName,
                    Email = user.EmailAddress,
                    PhotoLink = user.PhotoLink
                });
            }

            return _resource;
        }

        private async Task<Google.Apis.Drive.v3.DriveService> getServiceAsync(IGoogleAuthProvider auth)
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
