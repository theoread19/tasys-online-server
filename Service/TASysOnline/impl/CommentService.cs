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
    public class CommentService : ICommentService
    {
        private ICommentRepository _CommentRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public CommentService(ICommentRepository CommentRepository, IUriService uriService, IMapper mapper)
        {
            this._CommentRepository = CommentRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<int> CountAsync()
        {
            return await this._CommentRepository.CountAsync();
        }

        public async Task<Response> CreateCommentAsync(CommentRequest commentRequest)
        {

            var table = this._mapper.Map<CommentTable>(commentRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._CommentRepository.InsertAsync(table);
            await this._CommentRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Comment was created!" };
        }

        public async Task<Response> DeleteAllComment()
        {
            await this._CommentRepository.DeleteAllAsyn();
            await this._CommentRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Comment successfully!"
            };
        }

        public async Task<Response> DeleteComment(Guid[] commentId)
        {
            for (var i = 0; i < commentId.Length; i++)
            {
                await this._CommentRepository.DeleteAsync(commentId[i]);
            }

            await this._CommentRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Comment successfully!"
            };
        }

        public async Task<FilterResponse<List<CommentResponse>>> FilterCommentBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._CommentRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CommentResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CommentRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<CommentTable>, List<CommentResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CommentResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CommentResponse> FindByNameAsync(string name)
        {
            /*            var result = await this._CommentRepository.FindByNameAsync(name);

                        if (result == null)
                        {
                            return new CommentResponse
                            {
                                StatusCode = StatusCodes.Status404NotFound,
                                ResponseMessage = "Comment not Found!"
                            };
                        }

                        var response = this._mapper.Map<CommentResponse>(result);

                        response.StatusCode = StatusCodes.Status200OK;
                        response.ResponseMessage = "Comment is Found!";
                        return response;*/
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CommentResponse>> GetAllCommentAsync()
        {
            var tables = await this._CommentRepository.GetAllAsync();

            var responses = this._mapper.Map<List<CommentTable>, List<CommentResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<CommentResponse>>> GetAllCommentPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._CommentRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CommentResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CommentRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CommentResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<CommentTable>, List<CommentResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<CommentResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<CommentResponse> GetCommentById(Guid id)
        {
            var table = await this._CommentRepository.FindByIdAsync(id);
            var response = this._mapper.Map<CommentResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Comment successfully";
            return response;
        }

        public async Task<SearchResponse<List<CommentResponse>>> SearchCommentBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._CommentRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<CommentResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._CommentRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<CommentTable>, List<CommentResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<CommentResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateComment(CommentRequest commentRequest)
        {
            var table = await this._CommentRepository.FindByIdAsync(commentRequest.Id);

            table.ModifiedDate = DateTime.UtcNow;
            table.Content = commentRequest.Content;

            await this._CommentRepository.UpdateAsync(table);
            await this._CommentRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Comment successfully!" };
        }
    }
}
