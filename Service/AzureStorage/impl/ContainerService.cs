using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Service.TASysOnline;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.AzureStorage.impl
{
    public class ContainerService : IContainerService
    {
        private readonly string _accessKey;
        private readonly IConfiguration _configuration;
        private readonly CloudStorageAccount _cloudStorageAccount;
        private readonly CloudBlobClient _cloudBlobClient;
        private readonly IUriService _uriService;
        private readonly IMapper _mapper;
        private readonly IMediaService _mediaService;
        public ContainerService(IConfiguration configuration, IUriService uriService, IMapper mapper, IMediaService mediaService)
        {
            this._mapper = mapper;
            this._uriService = uriService;
            this._configuration = configuration;
            this._accessKey = this._configuration.GetConnectionString("AzureStorage");
            this._cloudStorageAccount = CloudStorageAccount.Parse(this._accessKey);
            this._cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
            this._mediaService = mediaService;
        }

        public async Task<Response> CreateContainerAsync(string containerName)
        {
            CloudBlobContainer cloudBlobContainer = this._cloudBlobClient.GetContainerReference(containerName);
            if (await cloudBlobContainer.ExistsAsync())
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Container are already exist!" };
            }

            await cloudBlobContainer.CreateAsync();
            await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Create container successfully!" };
        }

        public async Task<Response> DeleteAllContainer()
        {
            var blobContainers = new List<CloudBlobContainer>();
            BlobContinuationToken blobContinuationToken = null!;
            do
            {
                var containerSegment = await this._cloudBlobClient.ListContainersSegmentedAsync(blobContinuationToken);
                blobContainers.AddRange(containerSegment.Results);
                blobContinuationToken = containerSegment.ContinuationToken;
            } while (blobContinuationToken != null);

            foreach(var container in blobContainers)
            {
                await container.DeleteIfExistsAsync();
            }
            await this._mediaService.DeleteAllMedia();
            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Delete all containers successfully!" };
        }

        public async Task<Response> DeleteContainer(string[] containerNames)
        {
            foreach(var name in containerNames)
            {
                var medias = await this._mediaService.FindByContainerNameAsync(name);
                Guid[] mediaId = medias.Select(s => s.Id).ToArray();
                await this._mediaService.DeleteMediaAsync(mediaId);
                var cloudBlobContainer = this._cloudBlobClient.GetContainerReference(name);
                await cloudBlobContainer.DeleteIfExistsAsync();
            }

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Delete containers successfully!" };
        }

        public async Task<CloudBlobContainer> FindByNameAsync(string containerName)
        {
            /*            var cloudBlobContainer = this._cloudBlobClient.GetContainerReference(containerName);
                        if (await cloudBlobContainer.ExistsAsync())
                        {
                            return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Container are already exist!" };
                        }*/
            return null;
        }

        public async Task<List<ContainerResponse>> GetAllContainerAsync()
        {
            var blobContainers = new List<CloudBlobContainer>();
            BlobContinuationToken blobContinuationToken = null!;
            do
            {
                var containerSegment = await this._cloudBlobClient.ListContainersSegmentedAsync(blobContinuationToken);
                blobContainers.AddRange(containerSegment.Results);
                blobContinuationToken = containerSegment.ContinuationToken;
            } while (blobContinuationToken != null);

            var responses = this._mapper.Map<List<CloudBlobContainer>, List<ContainerResponse>>(blobContainers);

            return responses;
        }

        public async Task<PageResponse<List<ContainerResponse>>> GetAllContainerPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var datas = await this.GetAllContainerAsync();
            var totalData = datas.Count();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<ContainerResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            if (datas == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<ContainerResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pagedReponse = PaginationHelper.CreatePagedReponse<ContainerResponse>(datas, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        /// <summary>
        ///     Not use now
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <param name="route"></param>
        /// <returns></returns>
        public Task<SearchResponse<List<ContainerResponse>>> SearchContainerBy(Search searchRequest, string route)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> ChangeContainerNameAsync(string oldContainerName, string newContainerName)
        {
            CloudBlobContainer oldCloudBlobContainer = this._cloudBlobClient.GetContainerReference(oldContainerName);

            if (!(await oldCloudBlobContainer.ExistsAsync()))
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Container are not found!" };
            }

            CloudBlobContainer newCloudBlobContainer = this._cloudBlobClient.GetContainerReference(newContainerName);

            if (await newCloudBlobContainer.CreateIfNotExistsAsync())
            {
                await newCloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Container });
            }

            var medias = await this._mediaService.FindByContainerNameAsync(oldContainerName);
            var mediaUpdateRequest = new MediasRequest { Id = medias.Select(s => s.Id).ToArray(), targetContainer = newContainerName };
            await this._mediaService.MoveMediasAsync(mediaUpdateRequest);

            await oldCloudBlobContainer.DeleteAsync();
            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update container successfully!" };
        }
    }
}
