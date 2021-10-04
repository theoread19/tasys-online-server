using Azure.Storage.Blobs;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.AzureStorage
{
    public interface IBlobService
    {
        public Task<Response> UploadFileToBlobAsync(BlobContainerRequest blobContainerRequest);

        public Task<Response> DeleteBlobAsync(string fileName, string container);

        public Task<Response> MoveBlobAsync(string fileName, string oldContainer, string newContrianer);

        public Task<Response> ChangeNameBlobAsync(string cointaier, string oldFileName, string newFileName);

        public Task<Response> CopyBlobAsync(string targetContainer, string fileName, string oldContainer);

        public Task<BlobClient> GetBlobsAsync(string fileName, string container);
    }
}
