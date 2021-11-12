using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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
    public class TestService : ITestService
    {
        private ITestRepository _testRepository;

        private IUriService _uriService;

        private IMapper _mapper;

        public TestService(ITestRepository TestRepository, IUriService uriService, IMapper mapper)
        {
            this._testRepository = TestRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<Response> CreateTestAsync(TestRequest testRequest)
        {

            var table = this._mapper.Map<TestTable>(testRequest);
            table.Id = new Guid();
            table.CreatedDate = DateTime.UtcNow;

            await this._testRepository.InsertAsync(table);

            await this._testRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Test was created!" };
        }

        public async Task<Response> DeleteAllTest()
        {
            await this._testRepository.DeleteAllAsyn();
            await this._testRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete all Test successfully!"
            };
        }

        public async Task<Response> DeleteTest(Guid[] TestId)
        {
            for (var i = 0; i < TestId.Length; i++)
            {
                await this._testRepository.DeleteAsync(TestId[i]);
            }

            await this._testRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Test successfully!"
            };
        }

        public async Task<FilterResponse<List<TestResponse>>> FilterTestBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._testRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<TestResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._testRepository.FilterByAsync(validFilter);

            var pageData = this._mapper.Map<List<TestTable>, List<TestResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<TestResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<TestResponse>> GetAllTestAsync()
        {
            var tables = await this._testRepository.GetAllAsync();

            var responses = this._mapper.Map<List<TestTable>, List<TestResponse>>(tables);

            return responses;
        }

        public async Task<PageResponse<List<TestResponse>>> GetAllTestPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._testRepository.CountAsync();

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._testRepository.GetAllPadingAsync(validFilter);

            var pageData = this._mapper.Map<List<TestTable>, List<TestResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<TestResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<TestResponse> GetTestById(Guid id)
        {
            var table = await this._testRepository.FindTestByIdEagerAsync(id);

            if (table == null)
            {
                return new TestResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Test not found!" };
            }

            var response = this._mapper.Map<TestResponse>(table);
            response.StatusCode = StatusCodes.Status200OK;
            response.ResponseMessage = "Find Test successfully";
            return response;
        }

        public async Task<SearchResponse<List<TestResponse>>> SearchTestBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._testRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<TestResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._testRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<TestTable>, List<TestResponse>>(tables);
            var pagedReponse = PaginationHelper.CreatePagedReponse<TestResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateTest(TestRequest testRequest)
        {

            var table = await this._testRepository.FindByIdAsync(testRequest.Id);

            if (table == null)
            {
                return new TestResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Test not found!" };
            }

            table.Name = testRequest.Name;
            table.ModifiedDate = DateTime.UtcNow;
            table.Description = testRequest.Description;
            table.AllocatedTime = testRequest.AllocatedTime;
            table.Deadline = testRequest.Deadline;
            table.MaxAttempt = testRequest.MaxAttempt;
            table.MaxScore = testRequest.MaxScore;
            table.TotalQuestions = testRequest.TotalQuestions;

            await this._testRepository.UpdateAsync(table);
            await this._testRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Update Test successfully!" };
        }

        public async Task<FilterSearchResponse<List<TestResponse>>> FilterSearchTestBy(FilterSearch filterSearchRequest, string route)
        {
            var validFilter = new FilterSearch(filterSearchRequest.PageNumber,
                                        filterSearchRequest.PageSize,
                                        filterSearchRequest.SortBy!,
                                        filterSearchRequest.Order!,
                                        filterSearchRequest.FilterValue!,
                                        filterSearchRequest.FilterProperty!,
                                        filterSearchRequest.SearchValue!,
                                        filterSearchRequest.SearchProperty!);

            var data = await this._testRepository.GetAllAsync();

            var filterSearchData = FilterSearchUtil.FilterSearch<TestTable>(validFilter, data);

            var totalData = filterSearchData.Count;

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<TestResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var pageData = this._mapper.Map<List<TestTable>, List<TestResponse>>(filterSearchData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<TestResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }
    }
}
