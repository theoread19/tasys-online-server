using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Service.AzureStorage.impl
{
    public class BlobService : IBlobService
    {

        private readonly string _accessKey;
        private readonly IConfiguration _configuration;
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly IUriService _uriService;

        public BlobService(IConfiguration configuration, IUriService uriService)
        {
            this._uriService = uriService;
            this._configuration = configuration;
            this._accessKey = this._configuration.GetConnectionString("AzureStorage");
            this._cloudStorageAccount = CloudStorageAccount.Parse(this._accessKey);
            this._cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
        }

        public async Task<Response> ChangeNameBlobAsync(string cointaier, string oldFileName, string newFileName)
        {
            CloudBlobContainer oldCloudBlobContainer = this._cloudBlobClient.GetContainerReference(cointaier);
            if (!(await oldCloudBlobContainer.ExistsAsync()))
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Container are not found!" };
            }

            var oldCloudBlockBlob = oldCloudBlobContainer.GetBlockBlobReference(oldFileName);

            var newCloudBlockBlob = oldCloudBlobContainer.GetBlockBlobReference(newFileName);

            if (await newCloudBlockBlob.ExistsAsync())
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "file already exist!" };
            }

            await newCloudBlockBlob.StartCopyAsync(oldCloudBlockBlob);
            await oldCloudBlockBlob.DeleteAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = newCloudBlockBlob.Uri.AbsoluteUri };
        }

        public async Task<Response> CopyBlobAsync(string targetContainer, string fileName, string oldContainer)
        {
            CloudBlobContainer oldCloudBlobContainer = this._cloudBlobClient.GetContainerReference(oldContainer);
            if (!(await oldCloudBlobContainer.ExistsAsync()))
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Container are not found!" };
            }

            CloudBlobContainer newCloudBlobContainer = this._cloudBlobClient.GetContainerReference(targetContainer);

            if (await newCloudBlobContainer.CreateIfNotExistsAsync())
            {
                await newCloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            }

            var lastUnderLineIndex = fileName.LastIndexOf("_");
            var lastDotIdenx = fileName.LastIndexOf(".");
            var oldFileName = fileName.Substring(0, lastUnderLineIndex) + fileName.Substring(lastDotIdenx);

            var oldCloudBlockBlob = oldCloudBlobContainer.GetBlockBlobReference(oldFileName);

            var newCloudBlockBlob = newCloudBlobContainer.GetBlockBlobReference(fileName);
            await newCloudBlockBlob.StartCopyAsync(oldCloudBlockBlob);

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = newCloudBlockBlob.Uri.AbsoluteUri };
        }

        public async Task<Response> DeleteBlobAsync(string fileName, string container)
        {
            try
            {
                CloudBlobContainer cloudBlobContainer = this._cloudBlobClient.GetContainerReference(container);
                if (!(await cloudBlobContainer.ExistsAsync()))
                {
                    return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Container is not found!" };
                }

                if (fileName != null)
                {
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
                    await cloudBlockBlob.DeleteAsync();
                    return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Delete blob successfully!"};
                }

                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "file name is null!" };
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = @"Some thing wrong when upload : " + ex };
            }
        }

        public Task<BlobClient> GetBlobsAsync(string fileName, string container)
        {
            var cloudBlobClient = new BlobContainerClient(this._configuration.GetConnectionString("AzureStorage"), container);

            return Task.FromResult(cloudBlobClient.GetBlobClient(fileName));
        }

        public async Task<Response> MoveBlobAsync(string fileName, string oldContainer, string newContrianer)
        {
            CloudBlobContainer oldCloudBlobContainer = this._cloudBlobClient.GetContainerReference(oldContainer);
            if (!(await oldCloudBlobContainer.ExistsAsync()))
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Container are not found!" };
            }

            CloudBlobContainer newCloudBlobContainer = this._cloudBlobClient.GetContainerReference(newContrianer);

            if (await newCloudBlobContainer.CreateIfNotExistsAsync())
            {
                await newCloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            }

            var oldCloudBlockBlob = oldCloudBlobContainer.GetBlockBlobReference(fileName);

            var newCloudBlockBlob = newCloudBlobContainer.GetBlockBlobReference(fileName);
            await newCloudBlockBlob.StartCopyAsync(oldCloudBlockBlob);
            await oldCloudBlockBlob.DeleteAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = newCloudBlockBlob.Uri.AbsoluteUri };
        }

        public async Task<Response> UploadFileToBlobAsync(BlobContainerRequest blobContainerRequest)
        {
            try
            {
                CloudBlobContainer cloudBlobContainer = this._cloudBlobClient.GetContainerReference(blobContainerRequest.FileDirectory);
                if (await cloudBlobContainer.CreateIfNotExistsAsync())
                {
                    await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
                }

                if (blobContainerRequest.FileName != null && blobContainerRequest.FileContain != null)
                {
                    var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobContainerRequest.FileName);
                    cloudBlockBlob.Properties.ContentType = blobContainerRequest.FileType;
                    var data = Convert.FromBase64String(blobContainerRequest.FileContain);
                    await cloudBlockBlob.UploadFromByteArrayAsync(data, 0, data.Length);
                    return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = cloudBlockBlob.Uri.AbsoluteUri };
                }
                return new Response { StatusCode = StatusCodes.Status400BadRequest, ResponseMessage = "file name or contain is null!" };
            }
            catch (Exception ex)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Some thing wrong when upload : " + ex };
            }
        }

    }
}
