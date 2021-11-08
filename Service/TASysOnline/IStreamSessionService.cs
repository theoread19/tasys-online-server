using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IStreamSessionService
    {
        public Task<IEnumerable<StreamSessionResponse>> GetAllStreamSessionAsync();

        public Task<PageResponse<List<StreamSessionResponse>>> GetAllStreamSessionPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateStreamSessionAsync(StreamSessionRequest streamSessionRequest);

        public Task<StreamSessionResponse> GetStreamSessionById(Guid id);

        public Task<int> CountAsync();

        public Task<Response> UpdateStreamSession(StreamSessionRequest streamSessionRequest);

        public Task<Response> DeleteStreamSession(Guid[] streamSessionId);

        public Task<FilterResponse<List<StreamSessionResponse>>> FilterStreamSessionBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<StreamSessionResponse>>> SearchStreamSessionBy(Search searchRequest, string route);

        public Task<Response> DeleteAllStreamSession();

        public Task<IEnumerable<StreamSessionResponse>> GetComingStreamSessionAsync(DateTime now);

        public Task<FilterSearchResponse<List<StreamSessionResponse>>> FilterSearchStreamSessionBy(FilterSearch filterSearchRequest, string route);

        public Task<MemoryStream> ExportDBToExcel(List<UserAccountAuthRequest> learner);
    }
}
