using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IMediaService
    {

 //       public Task<PageResponse<List<MediaResponse>>> GetAllMediaPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateMediasAsync(MediaRequest[] mediaRequests);

        public Task<IEnumerable<MediaResponse>> FindByContainerNameAsync(string container);

        public Task<Response> MoveMediasAsync(MediasRequest mediaRequest);

        public Task<Response> ChangeMediaNameAsync(MediaRequest mediaRequest);

        public Task<Response> UpdateMediaAsync(MediaRequest mediaRequest);

        public Task<Response> CopyMediasAsync(MediasRequest mediaRequest);

        public Task<Response> DeleteMediaAsync(Guid[] mediaId);

        public Task<FilterResponse<List<MediaResponse>>> FilterMediaBy(Filter filterRequest, string route);

//        public Task<SearchResponse<List<MediaResponse>>> SearchMediaBy(Search searchRequest, string route);

        public Task<MediaResponse> FindMediaByIdAsync(Guid mediaId);

        public Task<Response> DeleteAllMedia();

        public Task<Stream> DownloadFileZipAsync(Guid[] Ids);
    }
}
