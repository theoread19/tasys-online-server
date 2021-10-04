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
    public class CartService : ICartService
    {
        private ICartRepository _CartRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public CartService(ICartRepository CartRepository, IUriService uriService, IMapper mapper)
        {
            this._CartRepository = CartRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            return await this._CartRepository.CountAsync();
        }

        public async Task<Response> CreateCartAsync(CartRequest CartRequest)
        {

            var table = this._mapper.Map<CartTable>(CartRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._CartRepository.InsertAsync(table);
            await this._CartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Cart was created!" };
        }

        public async Task<Response> DeleteAllCart()
        {
            await this._CartRepository.DeleteAllAsyn();
            await this._CartRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Cart successfully!"
            };
        }

        public async Task<Response> DeleteCart(Guid[] CartId)
        {
            for (var i = 0; i < CartId.Length; i++)
            {
                await this._CartRepository.DeleteAsync(CartId[i]);
            }

            await this._CartRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Cart successfully!"
            };
        }

        public async Task<FilterResponse<List<CartResponse>>> FilterCartBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._CartRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CartResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CartRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CartResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CartResponse> FindByNameAsync(string name)
        {
            /*            var result = await this._CartRepository.FindByNameAsync(name);

                        if (result == null)
                        {
                            return new CartResponse
                            {
                                StatusCode = StatusCodes.Status404NotFound,
                                ResponseMessage = "Cart not Found!"
                            };
                        }

                        var response = this._mapper.Map<CartResponse>(result);

                        response.StatusCode = StatusCodes.Status200OK;
                        response.ResponseMessage = "Cart is Found!";
                        return response;*/
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CartResponse>> GetAllCartAsync()
        {
            var tables = await this._CartRepository.GetAllAsync();

            var responses = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<CartResponse>>> GetAllCartPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._CartRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CartResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CartRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CartResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CartResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CartResponse> GetCartById(Guid id)
        {
            var table = await this._CartRepository.FindByIdAsync(id);
            var response = this._mapper.Map<CartResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Cart successfully";
            return response;
        }

        public async Task<SearchResponse<List<CartResponse>>> SearchCartBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._CartRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CartResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CartRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<CartTable>, List<CartResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CartResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateCart(CartRequest cartRequest)
        {
            var table = await this._CartRepository.FindByIdAsync(cartRequest.Id);

            table.ModifiedDate = DateTime.UtcNow;
            table.TotalCourse = cartRequest.TotalCourse;

            await this._CartRepository.UpdateAsync(table);
            await this._CartRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Cart successfully!" };
        }
    }
}
