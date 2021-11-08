using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface ISubjectService 
    {
        public Task<Response> CreateSubjectAsync(SubjectRequest subjectRequest);

        public Task<SubjectResponse> FindById(Guid subjectId);

        public Task<PageResponse<List<SubjectResponse>>> GetAllSubjectPagingAsync(Pagination paginationFilter, string route);

        public Task<IEnumerable<SubjectResponse>> GetAllSubjectAsync();

        public Task<SearchResponse<List<SubjectResponse>>> SearchSubjectBy(Search searchRequest, string route);

        public Task<Response> UpdateSubject(SubjectRequest subjectRequest);

        public Task<Response> DeleteSubject(Guid[] subjectId);
    }
}
