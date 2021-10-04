using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class DiscountService : IDiscountService
    {
        private IDiscountRepository _DiscountRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IUriService uriService, IMapper mapper)
        {
            this._DiscountRepository = discountRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            return await this._DiscountRepository.CountAsync();
        }

        public async Task<Response> CreateDiscountAsync(DiscountRequest discountRequest)
        {

            var table = this._mapper.Map<DiscountTable>(discountRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._DiscountRepository.InsertAsync(table);
            await this._DiscountRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Discount was created!" };
        }

        public async Task<Response> DeleteAllDiscount()
        {
            await this._DiscountRepository.DeleteAllAsyn();
            await this._DiscountRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Discount successfully!"
            };
        }

        public async Task<Response> DeleteDiscount(Guid[] discountId)
        {
            for (var i = 0; i < discountId.Length; i++)
            {
                await this._DiscountRepository.DeleteAsync(discountId[i]);
            }

            await this._DiscountRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Discount successfully!"
            };
        }

        public async Task<FilterResponse<List<DiscountResponse>>> FilterDiscountBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._DiscountRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._DiscountRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<DiscountTable>, List<DiscountResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<DiscountResponse> FindByNameAsync(string name)
        {
            /*            var result = await this._DiscountRepository.FindByNameAsync(name);

                        if (result == null)
                        {
                            return new DiscountResponse
                            {
                                StatusCode = StatusCodes.Status404NotFound,
                                ResponseMessage = "Discount not Found!"
                            };
                        }

                        var response = this._mapper.Map<DiscountResponse>(result);

                        response.StatusCode = StatusCodes.Status200OK;
                        response.ResponseMessage = "Discount is Found!";
                        return response;*/

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DiscountResponse>> GetAllDiscountAsync()
        {
            var tables = await this._DiscountRepository.GetAllAsync();

            var responses = this._mapper.Map<List<DiscountTable>, List<DiscountResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<DiscountResponse>>> GetAllDiscountPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._DiscountRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._DiscountRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<DiscountTable>, List<DiscountResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<DiscountResponse> FindDiscountByCourseId(Guid courseId)
        {
            var table = await this._DiscountRepository.FindDiscountTabeByCourseId(courseId);
            var response = this._mapper.Map<DiscountResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Discount successfully";
            return response;
        }

        public async Task<SearchResponse<List<DiscountResponse>>> SearchDiscountBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._DiscountRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._DiscountRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<DiscountTable>, List<DiscountResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<DiscountResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateDiscount(DiscountRequest discountRequest)
        {

            var table = await this._DiscountRepository.FindByIdAsync(discountRequest.Id);

            table.ModifiedDate = DateTime.UtcNow;
            table.Duration = discountRequest.Duration;
            table.Rate = discountRequest.Rate;
            table.Title = discountRequest.Title;

            await this._DiscountRepository.UpdateAsync(table);
            await this._DiscountRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Discount successfully!" };
        }
    }
}
