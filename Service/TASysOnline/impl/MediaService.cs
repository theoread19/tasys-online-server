using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.AzureStorage;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class MediaService : IMediaService
    {
        private IMediaRepository _mediaRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private IBlobService _blobService;

        public MediaService(IMediaRepository MediaRepository, IUriService uriService, IMapper mapper, IBlobService blobService)
        {
            this._mediaRepository = MediaRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._blobService = blobService;
        }

        public async Task<Response> CreateMediasAsync(MediaRequest[] mediaRequests)
        {
            foreach(var mediaRequest in mediaRequests)
            {
                var file = this._mapper.Map<BlobContainerRequest>(mediaRequest);
                var blobReponse = await this._blobService.UploadFileToBlobAsync(file);

                if (blobReponse.StatusCode != StatusCodes.Status201Created)
                {
                    return new Response { StatusCode = blobReponse.StatusCode, ResponseMessage = blobReponse.ResponseMessage + $@". Error in {mediaRequest.FileName}" };
                }

                var table = this._mapper.Map<MediaTable>(mediaRequest);

                table.CreatedDate = DateTime.UtcNow;
                table.Id = new Guid();
                table.Url = blobReponse.ResponseMessage;

                await this._mediaRepository.InsertAsync(table);
                await this._mediaRepository.SaveAsync();
            }
            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Media was created!" };
        }

        public async Task<Response> DeleteAllMedia()
        {
            await this._mediaRepository.DeleteAllAsyn();
            await this._mediaRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Media successfully!"
            };
        }

        public async Task<Response> DeleteMediaAsync(Guid[] MediaId)
        {
            for (var i = 0; i < MediaId.Length; i++)
            {
                var table = await this._mediaRepository.FindByIdAsync(MediaId[i]);
                if (table != null)
                {
                    var res = await this._blobService.DeleteBlobAsync(table.FileName!, table.Container!);

                    if (res.StatusCode != StatusCodes.Status200OK)
                    {
                        return new Response { StatusCode = res.StatusCode, ResponseMessage = res.ResponseMessage };
                    }

                    await this._mediaRepository.DeleteAsync(MediaId[i]);
                    await this._mediaRepository.SaveAsync();
                }
            }

            await this._mediaRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Media successfully!"
            };
        }

        public async Task<FilterResponse<List<MediaResponse>>> FilterMediaBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._mediaRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<MediaResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._mediaRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<MediaTable>, List<MediaResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<MediaResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<MediaResponse>> FindByContainerNameAsync(string container)
        {
            var tables = await this._mediaRepository.FindByContainerNameAsync(container);

            var responses = this._mapper.Map<List<MediaTable>, List<MediaResponse>>(tables);
            return responses;
        }

/*        public async Task<PageResponse<List<MediaResponse>>> GetAllMediaPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._mediaRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._mediaRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<MediaTable>, List<MediaResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<MediaResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }*/

/*        public async Task<SearchResponse<List<MediaResponse>>> SearchMediaBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._mediaRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<MediaResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._mediaRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<MediaTable>, List<MediaResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<MediaResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }*/

        public async Task<Response> MoveMediasAsync(MediasRequest mediaRequest)
        {
            foreach(var mediaId in mediaRequest.Id)
            {
                var table = await this._mediaRepository.FindByIdAsync(mediaId);

                if (table == null)
                {
                    return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Media not found!" };
                }

                var res = await this._blobService.MoveBlobAsync(table.FileName, table.Container, mediaRequest.targetContainer);

                if (res.StatusCode != StatusCodes.Status200OK)
                {
                    return new Response { StatusCode = res.StatusCode, ResponseMessage = res.ResponseMessage };
                }

                table.ModifiedDate = DateTime.UtcNow;
                table.Container = mediaRequest.targetContainer;
                table.Url = res.ResponseMessage;

                await this._mediaRepository.UpdateAsync(table);
                await this._mediaRepository.SaveAsync();
            }

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Move media successfully!" };
        }

        public async Task<Response> ChangeMediaNameAsync(MediaRequest mediaRequest)
        {
            var table = await this._mediaRepository.FindByIdAsync(mediaRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Media not found!" };
            }

            var res = await this._blobService.ChangeNameBlobAsync(table.Container, table.FileName, mediaRequest.FileName);

            if(res.StatusCode != StatusCodes.Status200OK)
            {
                return new Response { StatusCode = res.StatusCode, ResponseMessage = res.ResponseMessage };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.FileName = mediaRequest.FileName;
            table.Url = res.ResponseMessage;

            await this._mediaRepository.UpdateAsync(table);
            await this._mediaRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Change media name successfully!" };
        }

        public async Task<Response> CopyMediasAsync(MediasRequest mediaRequest)
        {
            foreach (var mediaId in mediaRequest.Id)
            {
                var table = await this._mediaRepository.FindByIdAsync(mediaId);

                if (table == null)
                {
                    return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Media not found!" };
                }

                table.Id = Guid.NewGuid();

                var lastDotIndex = table.FileName.LastIndexOf(".");
                var fileName = table.FileName.Substring(0, lastDotIndex)
                    + "_" + table.Id.ToString().Substring(0, 6) 
                    + table.FileName.Substring(lastDotIndex);

                var res = await this._blobService.CopyBlobAsync(mediaRequest.targetContainer, fileName, table.Container);

                if (res.StatusCode != StatusCodes.Status200OK)
                {
                    return new Response { StatusCode = res.StatusCode, ResponseMessage = res.ResponseMessage + $@". Error in {table.FileName}" };
                }

                table.CreatedDate = DateTime.UtcNow;
                table.FileName = fileName;
                table.Container = mediaRequest.targetContainer;
                table.Url = res.ResponseMessage;

                await this._mediaRepository.InsertAsync(table);
                await this._mediaRepository.SaveAsync();
            }

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Copy Media successfully!" };
        }

        public async Task<MediaResponse> FindMediaByIdAsync(Guid mediaId)
        {
            var media = await this._mediaRepository.FindByIdAsync(mediaId);

            if (media == null)
            {
                return new MediaResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Media not found!" };
            }

            var response = this._mapper.Map<MediaResponse>(media);

            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Fectching a media successfully!";

            return response;
        }

        public async Task<Stream> DownloadFileZipAsync(Guid[] Ids)
        {
            //string zipName = DateTime.Now.ToString().Replace("/", "_") + ".zip";

            var zipStream = new MemoryStream();
            using (var zip = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
            {
                foreach (var Id in Ids)
                {
                    var meida = await this._mediaRepository.FindByIdAsync(Id);
                    var blob = await this._blobService.GetBlobsAsync(meida.FileName, meida.Container);
                    var downBlob = await blob.DownloadAsync();
                    using (Stream entry = zip.CreateEntry(meida.FileName).Open())
                    {
                        await downBlob.Value.Content.CopyToAsync(entry);
                    };
                }
            }
            zipStream.Position = 0;
            return zipStream;
        }

        public async Task<Response> UpdateMediaAsync(MediaRequest mediaRequest)
        {
            var table = await this._mediaRepository.FindByIdAsync(mediaRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Media not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.SourceID = mediaRequest?.SourceID;
            table.Title = mediaRequest?.Title;

            await this._mediaRepository.UpdateAsync(table);
            await this._mediaRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update media successfully!" };
        }
    }
}
