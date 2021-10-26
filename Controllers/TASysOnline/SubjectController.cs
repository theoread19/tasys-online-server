using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        public ISubjectService _subjectService;

        public SubjectController( ISubjectService subjectService)
        {
            this._subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubject()
        {
            var responses = await this._subjectService.GetAllSubjectAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectRequest subjectRequest)
        {
            var response = await this._subjectService.CreateSubjectAsync(subjectRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("paging")]
        public async Task<IActionResult> GetAllSubjectPaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._subjectService.GetAllSubjectPagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("search")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;     
            var responses = await this._subjectService.SearchSubjectBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> UpdateSubject([FromBody] SubjectRequest subjectRequest)
        {
            var response = await this._subjectService.UpdateSubject(subjectRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteSubject([FromBody] Guid[] subjectId)
        {
            var response = await this._subjectService.DeleteSubject(subjectId);

            return StatusCode(response.StatusCode, response);
        }
    }
}
