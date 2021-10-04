using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ICurriCulumService
    {
        public Task<IEnumerable<CurriCulumResponse>> GetAllCurriCulumAsync();

        public Task<PageResponse<List<CurriCulumResponse>>> GetAllCurriCulumPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateCurriCulumAsync(CurriCulumRequest curriCulumRequest);

        public Task<CurriCulumResponse> FindByNameAsync(string name);

        public Task<CurriCulumResponse> GetCurriCulumById(Guid id);

        public Task<int> CountAsync();

        public Task<Response> UpdateCurriCulum(CurriCulumRequest curriCulumRequest);

        public Task<Response> DeleteCurriCulum(Guid[] curriCulumId);

        public Task<FilterResponse<List<CurriCulumResponse>>> FilterCurriCulumBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<CurriCulumResponse>>> SearchCurriCulumBy(Search searchRequest, string route);

        public Task<Response> DeleteAllCurriCulum();
    }
}
