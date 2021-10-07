﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.TASysOnline;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllCourse()
        {
            var responses = await this._courseService.GetAllCourseAsync();

            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpGet]
        [Route("paging")]
//        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllCoursePaging([FromQuery] Pagination paginationFilter)
        {
            var route = Request.Path.Value;
            var pagedReponse = await this._courseService.GetAllCoursePagingAsync(paginationFilter, route);
            return StatusCode(pagedReponse.StatusCode, pagedReponse);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetCourseById(Guid id)
        {
            var response = await this._courseService.GetCourseById(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Route("search")]
//        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> SearchSubject([FromQuery] Search searchRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._courseService.SearchCourseBy(searchRequest, route);
            return StatusCode(responses.StatusCode, responses);
        }

        [HttpGet]
        [Route("filter")]
//        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterCourse([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._courseService.FilterCourseBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> CreateCourse([FromBody] CourseRequest courseRequest)
        {
            var response = await this._courseService.CreateCourseAsync(courseRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseRequest courseRequest)
        {
            var response = await this._courseService.UpdateCourse(courseRequest);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Instructor + "," + Roles.Admin)]
        public async Task<IActionResult> DeleteCourse([FromBody] Guid[] courseId)
        {
            var response = await this._courseService.DeleteCourse(courseId);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DeleteAllCourse()
        {
            var response = await this._courseService.DeleteAllCourse();
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("generate-data")]
        public async Task<IActionResult> Generate()
        {
            await this._courseService.GenerateData();
            return Ok();
        }
    }
}
