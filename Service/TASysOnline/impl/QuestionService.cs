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
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository _questionRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private readonly ITestService _testService;

        public QuestionService(IQuestionRepository questionRepository, IUriService uriService, IMapper mapper, ITestService testService)
        {
            this._questionRepository = questionRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._testService = testService;
        }

        public async Task<Response> CreateQuestionAsync(QuestionRequest questionRequest)
        {
            var test = await this._testService.GetTestById(questionRequest.TestId);

            if (test.StatusCode == StatusCodes.Status404NotFound)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Test not found!" };
            }

            var countQuestionOfTest = test.QuestionResponses.Count();

            if (test.TotalQuestions <= countQuestionOfTest)
            {
                return new Response { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "Test is full of questions!" };
            }

            var table = this._mapper.Map<QuestionTable>(questionRequest);
            table.CreatedDate = DateTime.UtcNow;
            table.Id = new Guid();

            await this._questionRepository.InsertAsync(table);
            await this._questionRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Question was created!" };
        }

        public async Task<Response> DeleteAllQuestion()
        {
            await this._questionRepository.DeleteAllAsyn();
            await this._questionRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Question successfully!"
            };
        }

        public async Task<Response> DeleteQuestion(Guid[] QuestionId)
        {
            for (var i = 0; i < QuestionId.Length; i++)
            {
                await this._questionRepository.DeleteAsync(QuestionId[i]);
            }

            await this._questionRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Question successfully!"
            };
        }

        public async Task<FilterResponse<List<QuestionResponse>>> FilterQuestionBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._questionRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._questionRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionAsync()
        {
            var tables = await this._questionRepository.GetAllAsync();

            var responses = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<QuestionResponse>>> GetAllQuestionPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._questionRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._questionRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<QuestionResponse> GetQuestionById(Guid id)
        {
            var table = await this._questionRepository.FindQuestionByIdEagerAsync(id);

            if (table == null)
            {
                return new QuestionResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Questions not found!" };
            }

            var response = this._mapper.Map<QuestionResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Question successfully";
            return response;
        }

        public async Task<SearchResponse<List<QuestionResponse>>> SearchQuestionBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._questionRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._questionRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateQuestion(QuestionRequest questionRequest)
        {
            var table = await this._questionRepository.FindByIdAsync(questionRequest.Id);

            if (table == null)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Questions not found!" };
            }

            table.ModifiedDate = DateTime.UtcNow;
            table.Score = questionRequest.Score;
            table.TotalCorrectAnswer = questionRequest.TotalCorrectAnswer;
            table.Content = questionRequest.Content;

            await this._questionRepository.UpdateAsync(table);
            await this._questionRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Question successfully!" };
        }

        public async Task<IEnumerable<QuestionResponse>> FindQuestionByTestId(Guid testId)
        {
            var tables = await this._questionRepository.FindQuestionByTestIdEagerAsync(testId);

            var responses = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            return responses;
        }

        public async Task<FilterSearchResponse<List<QuestionResponse>>> FilterSearchQuestionBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber,
                                        filterSearchRequest.PageSize,
                                        filterSearchRequest.SortBy!,
                                        filterSearchRequest.Order!,
                                        filterSearchRequest.FilterValue!,
                                        filterSearchRequest.FilterProperty!,
                                        filterSearchRequest.SearchValue!,
                                        filterSearchRequest.SearchProperty!);

            var data = await this._questionRepository.GetAllAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<QuestionTable>(validFilter, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }
    }
}
