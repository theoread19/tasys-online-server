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
        private IQuestionRepository _QuestionRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        private readonly IAnswerService _answerService;

        private readonly ITestService _testService;

        public QuestionService(IQuestionRepository QuestionRepository, IUriService uriService, IMapper mapper, IAnswerService answerService, ITestService testService)
        {
            this._QuestionRepository = QuestionRepository;
            this._uriService = uriService;
            this._mapper = mapper;
            this._answerService = answerService;
            this._testService = testService;
        }

        public async Task<int> CountAsync()
        {
            return await this._QuestionRepository.CountAsync();
        }

        public async Task<Response> CreateQuestionAsync(QuestionRequest questionRequest)
        {
            var test = await this._testService.GetTestById(questionRequest.TestId);

            if (test == null)
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
            var question = await this._QuestionRepository.InsertAsync(table);
            var answerRequests = questionRequest.AnswerRequests.ToList().Distinct();
            if (question.TotalCorrectAnswer != answerRequests.Where(w => w.IsCorrect == true).Count())
            {
                return new Response { StatusCode = StatusCodes.Status400BadRequest, ResponseMessage = "Total number of correct answers and correct answers are inconsistent!" };
            }
            await this._QuestionRepository.SaveAsync();
            foreach (var answerRequest in answerRequests)
            {
                answerRequest.QuestionId = question.Id;
                await this._answerService.CreateAnswerAsync(answerRequest);
            }

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Question was created!" };
        }

        public async Task<Response> DeleteAllQuestion()
        {
            await this._QuestionRepository.DeleteAllAsyn();
            await this._QuestionRepository.SaveAsync();
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
                await this._QuestionRepository.DeleteAsync(QuestionId[i]);
            }

            await this._QuestionRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Question successfully!"
            };
        }

        public async Task<FilterResponse<List<QuestionResponse>>> FilterQuestionBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._QuestionRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._QuestionRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<QuestionResponse>> GetAllQuestionAsync()
        {
            var tables = await this._QuestionRepository.GetAllAsync();

            var responses = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<QuestionResponse>>> GetAllQuestionPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._QuestionRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._QuestionRepository.GetAllPadingAsync(validFilter);

            if (tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<QuestionResponse> GetQuestionById(Guid id)
        {
            var table = await this._QuestionRepository.FindQuestionByIdEagerAsync(id);
            var response = this._mapper.Map<QuestionResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Question successfully";
            return response;
        }

        public async Task<SearchResponse<List<QuestionResponse>>> SearchQuestionBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._QuestionRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._QuestionRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<QuestionTable>, List<QuestionResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<QuestionResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateQuestion(QuestionRequest questionRequest)
        {
            var table = await this._QuestionRepository.FindByIdAsync(questionRequest.Id);

            table.ModifiedDate = DateTime.UtcNow;
            table.Score = questionRequest.Score;
            table.TotalCorrectAnswer = questionRequest.TotalCorrectAnswer;
            table.Score = questionRequest.Score;
            table.Content = questionRequest.Content;

            await this._QuestionRepository.UpdateAsync(table);
            await this._QuestionRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Question successfully!" };
        }

        public async Task<IEnumerable<QuestionResponse>> FindQuestionByTestId(Guid testId)
        {
            var tables = await this._QuestionRepository.FindQuestionByTestIdEagerAsync(testId);

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

            var data = await this._QuestionRepository.GetAllAsync();

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
