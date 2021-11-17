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
        private IAnswerRepository _answerRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private IQuestionService _questionService;
        public AnswerService(IAnswerRepository answerRepository, IUriService uriService, IMapper mapper, IQuestionService questionService)
        {
            this._answerRepository = answerRepository;
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
            await this._answerRepository.InsertAsync(table);
            await this._answerRepository.SaveAsync();

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
            await this._answerRepository.DeleteAllAsyn();
            await this._answerRepository.SaveAsync();
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
                var table = await this._answerRepository.FindByIdAsync(answerId[i]);

                if (table != null && table.IsCorrect)
                {
                    var question = await this._questionService.GetQuestionById(table.QuestionId);

                    if (question.StatusCode == StatusCodes.Status404NotFound)
                    {
                        return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Question not found!" };
                    }

                    await this._questionService.UpdateQuestion(new QuestionRequest
                    {
                        Id = question.Id,
                        TotalCorrectAnswer = question.TotalCorrectAnswer - 1,
                        Content = question.Content,
                        Score = question.Score,
                        TestId = question.TestId
                    });
                }

                await this._answerRepository.DeleteAsync(answerId[i]);
            }

            await this._answerRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Answer successfully!"
            };
        }

        public async Task<FilterResponse<List<AnswerResponse>>> FilterAnswerBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._answerRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._answerRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<AnswerResponse>> GetAllAnswerAsync()
        {
            var tables = await this._answerRepository.GetAllAsync();
            var responses = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);
            return responses;
        }

        public async Task<PageResponse<List<AnswerResponse>>> GetAllAnswerPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._answerRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._answerRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<SearchResponse<List<AnswerResponse>>> SearchAnswerBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._answerRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._answerRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<AnswerTable>, List<AnswerResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<AnswerResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateAnswer(AnswerRequest answerRequest)
        {
            var table = await this._answerRepository.FindByIdAsync(answerRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Answer not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.Content = answerRequest.Content;
            table.IsCorrect = answerRequest.IsCorrect;

            await this._answerRepository.UpdateAsync(table);
            await this._answerRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Answer successfully!" };
        }
    }
}
