using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IBillService
    {
        public Task<IEnumerable<BillResponse>> GetAllBillAsync();

        public Task<PageResponse<List<BillResponse>>> GetAllBillPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateBillAsync(BillRequest billRequest);

        public Task<BillResponse> GetBillById(Guid id);

        public Task<Response> DeleteBill(Guid[] billId);

        public Task<FilterResponse<List<BillResponse>>> FilterBillBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<BillResponse>>> SearchBillBy(Search searchRequest, string route);

        public Task<Response> DeleteAllBill();
    }
}
