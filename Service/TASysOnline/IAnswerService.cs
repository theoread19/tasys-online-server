﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IAnswerService
    {
        public Task<IEnumerable<AnswerResponse>> GetAllAnswerAsync();

        public Task<PageResponse<List<AnswerResponse>>> GetAllAnswerPagingAsync(Pagination paginationFilter, string route);

        public Task<Response> CreateAnswerAsync(AnswerRequest answerRequest);

        public Task<AnswerResponse> FindByNameAsync(string name);

        public Task<AnswerResponse> GetAnswerById(Guid id, Guid userId);

        public Task<int> CountAsync();

        public Task<Response> UpdateAnswer(AnswerRequest answerRequest);

        public Task<Response> DeleteAnswer(Guid[] answerId);

        public Task<FilterResponse<List<AnswerResponse>>> FilterAnswerBy(Filter filterRequest, string route);

        public Task<SearchResponse<List<AnswerResponse>>> SearchAnswerBy(Search searchRequest, string route);

        public Task<IEnumerable<AnswerResponse>> FindByQuestionId(Guid questionId);
        public Task<Response> DeleteAllAnswer();
    }
}
