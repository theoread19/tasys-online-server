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
    public class MessageService : IMessageService
    {
        private IMessageRepository _messageRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public MessageService(IMessageRepository MessageRepository, IUriService uriService, IMapper mapper)
        {
            this._messageRepository = MessageRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<Response> CreateMessageAsync(MessageRequest messageRequest)
        {

            var table = this._mapper.Map<MessageTable>(messageRequest);
            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._messageRepository.InsertAsync(table);
            await this._messageRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Message was created!" };
        }

        public async Task<Response> DeleteAllMessage()
        {
            await this._messageRepository.DeleteAllAsyn();
            await this._messageRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Message successfully!"
            };
        }

        public async Task<Response> DeleteMessage(Guid[] messageId)
        {
            for (var i = 0; i < messageId.Length; i++)
            {
                await this._messageRepository.DeleteAsync(messageId[i]);
            }

            await this._messageRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Message successfully!"
            };
        }

        public async Task<FilterResponse<List<MessageResponse>>> FilterMessageBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._messageRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<MessageResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._messageRepository.GetAllMessageEagerLoad();

            var filterData = FilterUtils.Filter<MessageTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<MessageTable>, List<MessageResponse>>(filterData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<MessageResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<MessageResponse>> GetAllMessageAsync()
        {
            var tables = await this._messageRepository.GetAllAsync();

            var responses = this._mapper.Map<List<MessageTable>, List<MessageResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<MessageResponse>>> GetAllMessagePagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._messageRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._messageRepository.GetAllMessageEagerLoad();

            var pagedData = PagedUtil.Pagination<MessageTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<MessageTable>, List<MessageResponse>>(pagedData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<MessageResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<MessageResponse> GetMessageById(Guid id)
        {
            var table = await this._messageRepository.FindByIdAsync(id);

            if (table == null)
            {
                return new MessageResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Comment not found!" };
            }

            var response = this._mapper.Map<MessageResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Message successfully";
            return response;
        }

        public async Task<SearchResponse<List<MessageResponse>>> SearchMessageBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._messageRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<MessageResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._messageRepository.GetAllMessageEagerLoad();

            var searchData = SearchUtils.Search<MessageTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<MessageTable>, List<MessageResponse>>(searchData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<MessageResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateMessage(MessageRequest messageRequest)
        {
            var table = await this._messageRepository.FindByIdAsync(messageRequest.Id);

            if (table == null)
            {
                return new MessageResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Comment not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.Content = messageRequest.Content;

            await this._messageRepository.UpdateAsync(table);
            await this._messageRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Message successfully!" };
        }
    }
}
