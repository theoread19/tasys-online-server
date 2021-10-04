using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IDiscountService
    {
        public Task<IEnumerable<DiscountResponse>> GetAllDiscountAsync();

        public Task<PageResponse<List<DiscountResponse>>> GetAllDiscountPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateDiscountAsync(DiscountRequest discountRequest);

        public Task<DiscountResponse> FindByNameAsync(string name);

        public Task<DiscountResponse> FindDiscountByCourseId(Guid id);

        public Task<int> CountAsync();

        public Task<Response> UpdateDiscount(DiscountRequest discountRequest);

        public Task<Response> DeleteDiscount(Guid[] discountId);

        public Task<FilterResponse<List<DiscountResponse>>> FilterDiscountBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<DiscountResponse>>> SearchDiscountBy(Search searchRequest, string route);

        public Task<Response> DeleteAllDiscount();
    }
}
