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
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public PostService(IPostRepository postRepository, IUriService uriService, IMapper mapper)
        {
            this._postRepository = postRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<Response> CreatePostAsync(PostRequest postRequest)
        {

            var table = this._mapper.Map<PostTable>(postRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._postRepository.InsertAsync(table);
            await this._postRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Post was created!" };
        }

        public async Task<Response> DeleteAllPost()
        {
            await this._postRepository.DeleteAllAsyn();
            await this._postRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Post successfully!"
            };
        }

        public async Task<Response> DeletePost(Guid[] PostId)
        {
            for (var i = 0; i < PostId.Length; i++)
            {
                await this._postRepository.DeleteAsync(PostId[i]);
            }

            await this._postRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Post successfully!"
            };
        }

        public async Task<FilterResponse<List<PostResponse>>> FilterPostBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._postRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._postRepository.GetAllPostEagerLoadAsync();

            var filterData = FilterUtils.Filter<PostTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(filterData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<FilterSearchResponse<List<PostResponse>>> FilterSearchPostBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber, filterSearchRequest.PageSize, filterSearchRequest.SortBy!, filterSearchRequest.Order!, filterSearchRequest.FilterValue!, filterSearchRequest.FilterProperty!, filterSearchRequest.SearchValue!, filterSearchRequest.SearchProperty!);

            var data = await this._postRepository.GetAllPostEagerLoadAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<PostTable>(filterSearchRequest, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<PostResponse>> GetAllPostAsync()
        {
            var tables = await this._postRepository.GetAllAsync();

            var responses = this._mapper.Map<List<PostTable>, List<PostResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<PostResponse>>> GetAllPostPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._postRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._postRepository.GetAllPostEagerLoadAsync();

            var pagedData = PagedUtil.Pagination<PostTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(pagedData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<PostResponse> GetPostById(Guid id)
        {
            var table = await this._postRepository.FindByIdAsync(id);

            if (table == null)
            {
                return new PostResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Post not found!" };
            }

            var response = this._mapper.Map<PostResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Post successfully";
            return response;
        }

        public async Task<SearchResponse<List<PostResponse>>> SearchPostBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._postRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._postRepository.GetAllPostEagerLoadAsync();

            var searchData = SearchUtils.Search<PostTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(searchData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdatePost(PostRequest postRequest)
        {
            var table = await this._postRepository.FindByIdAsync(postRequest.Id);

            if (table == null)
            {
                return new PostResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Post not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.Content = postRequest.Content;
            table.Title = postRequest.Title;
            await this._postRepository.UpdateAsync(table);
            await this._postRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Post successfully!" };
        }
    }
}
