using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.AzureStorage
{
    public interface IContainerService
    {
        public Task<Response> CreateContainerAsync(string containerName);

        public Task<List<ContainerResponse>> GetAllContainerAsync();

        public Task<CloudBlobContainer> FindByNameAsync(string containerName);

        public Task<PageResponse<List<ContainerResponse>>> GetAllContainerPagingAsync(Pagination paginationFilter, string route);

        public Task<SearchResponse<List<ContainerResponse>>> SearchContainerBy(Search searchRequest, string route);

        public Task<Response> ChangeContainerNameAsync(string oldContainerName, string newContainerName);

        public Task<Response> DeleteContainer(string[] containerNames);

        public Task<Response> DeleteAllContainer();
    }
}
