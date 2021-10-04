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
        private IPostRepository _PostRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public PostService(IPostRepository PostRepository, IUriService uriService, IMapper mapper)
        {
            this._PostRepository = PostRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            return await this._PostRepository.CountAsync();
        }

        public async Task<Response> CreatePostAsync(PostRequest postRequest)
        {

            var table = this._mapper.Map<PostTable>(postRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._PostRepository.InsertAsync(table);
            await this._PostRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Post was created!" };
        }

        public async Task<Response> DeleteAllPost()
        {
            await this._PostRepository.DeleteAllAsyn();
            await this._PostRepository.SaveAsync();
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
                await this._PostRepository.DeleteAsync(PostId[i]);
            }

            await this._PostRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Post successfully!"
            };
        }

        public async Task<FilterResponse<List<PostResponse>>> FilterPostBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._PostRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._PostRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<PostResponse> FindByTitleAsync(string title)
        {
            /*            var result = await this._PostRepository.FindByTitleAsync(title);

                        if (result == null)
                        {
                            return new PostResponse
                            {
                                StatusCode = StatusCodes.Status404NotFound,
                                ResponseMessage = "Post not Found!"
                            };
                        }

                        var response = this._mapper.Map<PostResponse>(result);

                        response.StatusCode = StatusCodes.Status200OK;
                        response.ResponseMessage = "Post is Found!";
                        return response;*/

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostResponse>> GetAllPostAsync()
        {
            var tables = await this._PostRepository.GetAllAsync();

            var responses = this._mapper.Map<List<PostTable>, List<PostResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<PostResponse>>> GetAllPostPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._PostRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._PostRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<PostResponse> GetPostById(Guid id)
        {
            var table = await this._PostRepository.FindByIdAsync(id);
            var response = this._mapper.Map<PostResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Post successfully";
            return response;
        }

        public async Task<SearchResponse<List<PostResponse>>> SearchPostBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._PostRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<PostResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._PostRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<PostTable>, List<PostResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<PostResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdatePost(PostRequest postRequest)
        {
            var table = await this._PostRepository.FindByIdAsync(postRequest.Id);

            table.ModifiedDate = DateTime.UtcNow;
            table.Content = postRequest.Content;
            table.Title = postRequest.Title;
            await this._PostRepository.UpdateAsync(table);
            await this._PostRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Post successfully!" };
        }
    }
}
