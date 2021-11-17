using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class AnswerService : IAnswerService
    {
        private IAnswerRepository _AnswerRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private IQuestionService _questionService;
        public AnswerService(IAnswerRepository AnswerRepository, IUriService uriService, IMapper mapper, IQuestionService questionService)
        {
            this._AnswerRepository = AnswerRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._questionService = questionService;
        }

        public async Task<Response> CreateAnswerAsync(AnswerRequest answerRequest)
        {
            var question = await this._questionService.GetQuestionById(answerRequest.QuestionId);

            if (question.StatusCode == StatusCodes.Status404NotFound)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Question not found!" };
            }

            var table = this._mapper.Map<AnswerTable>(answerRequest);

            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();
            await this._AnswerRepository.InsertAsync(table);
            await this._AnswerRepository.SaveAsync();

            if (table.IsCorrect)
            {
                question.TotalCorrectAnswer += 1;
                await this._questionService.UpdateQuestion(new QuestionRequest {
                Id = question.Id,
                TotalCorrectAnswer = question.TotalCorrectAnswer,
                Content = question.Content,
                Score = question.Score,
                TestId = question.TestId});
            }

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Answer was created!" };
        }

        public async Task<Response> DeleteAllAnswer()
        {
            await this._AnswerRepository.DeleteAllAsyn();
            await this._AnswerRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Answer successfully!"
            };
        }

        public async Task<Response> DeleteAnswer(Guid[] answerId)
        {
            for (var i = 0; i < answerId.Length; i++)
            {
                var table = await this._AnswerRepository.FindByIdAsync(answerId[i]);

                if (table.IsCorrect)
                {
                    var question = await this._questionService.GetQuestionById(table.QuestionId);
                    await this._questionService.UpdateQuestion(new QuestionRequest
                    {
                        Id = question.Id,
                        TotalCorrectAnswer = question.TotalCorrectAnswer - 1,
                        Content = question.Content,
                        Score = question.Score,
                        TestId = question.TestId
                    });
                }

                await this._AnswerRepository.DeleteAsync(answerId[i]);
            }

            await this._AnswerRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Answer successfully!"
            };
        }

        public async Task<FilterResponse<List<AnswerResponse>>> FilterAnswerBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._AnswerRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._AnswerRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<AnswerResponse>> GetAllAnswerAsync()
        {
            var tables = await this._AnswerRepository.GetAllAsync();
            var responses = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);
            return responses;
        }

        public async Task<PageResponse<List<AnswerResponse>>> GetAllCartPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._AnswerRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._AnswerRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<SearchResponse<List<AnswerResponse>>> SearchAnswerBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._AnswerRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._AnswerRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateAnswer(AnswerRequest answerRequest)
        {
            var table = await this._AnswerRepository.FindByIdAsync(answerRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Answer not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.Content = answerRequest.Content;
            table.IsCorrect = answerRequest.IsCorrect;

            await this._AnswerRepository.UpdateAsync(table);
            await this._AnswerRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Answer successfully!" };
        }
    }
}
