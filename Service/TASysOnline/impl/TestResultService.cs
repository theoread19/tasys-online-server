﻿using AutoMapper;
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
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _testResultRepository;

        private readonly ITestService _testService;

        private readonly IQuestionService _questionService;

        private readonly IUriService _uriService;

        private readonly IUserAccountService _userAccountService;

        private readonly IMapper _mapper;

        public TestResultService(ITestService testService, ITestResultRepository testResultRepository, IQuestionService questionService, IUriService uriService, IMapper mapper, IUserAccountService userAccountService)
        {
            this._testResultRepository = testResultRepository;
            this._testService = testService;
            this._questionService = questionService;
            this._mapper = mapper;
            this._uriService = uriService;
            this._userAccountService = userAccountService;
        }

        public async Task<TestResultResponse> CalculateTestResult(DoTestRequest doTestRequest)
        {
            var user = await this._userAccountService.FindByIdAsync(doTestRequest.UserId);

            if (user.StatusCode == StatusCodes.Status404NotFound)
            {
                return new TestResultResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var test = await this._testService.GetTestById(doTestRequest.TestId);

            if (test.StatusCode == StatusCodes.Status404NotFound)
            {
                return new TestResultResponse { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Test not found!" };
            }
            var totalAttempt = await this._testResultRepository.CountTestResultByUserIdAndTestId(doTestRequest.UserId, doTestRequest.TestId);

            if (totalAttempt >= test.MaxAttempt && !doTestRequest.IsPractice)
            {
                return new TestResultResponse { StatusCode = StatusCodes.Status500InternalServerError, ResponseMessage = "can't attempt this test" };
            }

            var questions = (await this._questionService.FindQuestionByTestId(doTestRequest.TestId)).ToList();
            var questionsOfUser = doTestRequest.QuestionRequest.ToList();
            float toltalScorce = 0;

            for (var i = 0; i < questionsOfUser.Count(); i++)
            {
                var answerOfUser = questionsOfUser[i].AnswerRequests.Select(s => s.Id).ToList();
                var answer = questions.Where(w => w.Id == questionsOfUser[i].Id).FirstOrDefault().AnswerResponses.ToList();
                if (answerOfUser.Count > 0)
                {
                    var vaildAnswer = answer.Where(w => answerOfUser.Contains(w.Id));
                    var countCorrectAnswer = vaildAnswer.Where(w => w.IsCorrect == true).Count();
                    var countIncorrectAnswer = vaildAnswer.Count() - countCorrectAnswer;
                    var pointOfQuestion = ((float)countCorrectAnswer / questions[i].TotalCorrectAnswer) * questions[i].Score;

                    toltalScorce += pointOfQuestion;
                }
            }

            var table = await this._testResultRepository.InsertAsync(
                new TestResultTable{
                    Id = Guid.NewGuid(),
                    Score = toltalScorce,
                    TestId = doTestRequest.TestId,
                    CreatedDate = DateTime.UtcNow,
                    IsPractice = doTestRequest.IsPractice,
                    UserAccountId = doTestRequest.UserId
                });

            await this._testResultRepository.SaveAsync();

            var response = this._mapper.Map<TestResultResponse>(table);
            response.QuestionResponses = this._mapper.Map<List<QuestionRequest>, List<QuestionResponse>>(doTestRequest.QuestionRequest!);
            response.UserAccountResponse = user;
            response.ResponseMessage = "Test result was stored!";
            response.StatusCode = StatusCodes.Status201Created;
            return response;
        }

        public async Task<FilterResponse<List<TestResultResponse>>> FilterTestResultBy(Filter filterRequest, string route)
        {
            var validFilter = new Filter(filterRequest.PageNumber, filterRequest.PageSize, filterRequest.SortBy!, filterRequest.Order!, filterRequest.Value!, filterRequest.Property!);

            var totalData = await this._testResultRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<TestResultResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._testResultRepository.GetAllTestResultTablesEagerLoad();

            var filterData = FilterUtils.Filter<TestResultTable>(validFilter, tables);

            var pageData = this._mapper.Map<List<TestResultTable>, List<TestResultResponse>>(filterData);

            var pagedReponse = PaginationHelper.CreatePagedReponse<TestResultResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<IEnumerable<TestResultResponse>> GetTestResultByTestIdAndUserIdAsync(Guid userId, Guid testId)
        {
            var tables = await this._testResultRepository.GetTestResultByUserIdAndTestId(userId, testId);

            var response = this._mapper.Map<List<TestResultTable>, List<TestResultResponse>>(tables);

            return response;
        }
    }
}
