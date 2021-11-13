using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {
        private readonly IGenerateService _generateService;

        public GenerateController(IGenerateService generateService)
        {
            this._generateService = generateService;
        }

        [HttpPost]
        [Route("generate-all")]
        public async Task<IActionResult> GenerateAll()
        {
            await this._generateService.GenerateRoleData();
            await this._generateService.GenerateSubjectData();
            await this._generateService.GenerateUserAccountData();
            await this._generateService.GenerateCourseData();
            await this._generateService.GenerateLessonData();
            await this._generateService.GenerateTestData();
            await this._generateService.GenerateQuestionsData();
            await this._generateService.GenerateStreamData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-role-data")]
        public async Task<IActionResult> GenerateRole()
        {
            await this._generateService.GenerateRoleData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-user-data")]
        public async Task<IActionResult> GenerateUser()
        {
            await this._generateService.GenerateUserAccountData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-subject-data")]
        public async Task<IActionResult> GenerateSubject()
        {
            await this._generateService.GenerateSubjectData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-course-data")]
        public async Task<IActionResult> GenerateCourse()
        {
            await this._generateService.GenerateCourseData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-lesson-data")]
        public async Task<IActionResult> GenerateLesson()
        {
            await this._generateService.GenerateLessonData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-test-data")]
        public async Task<IActionResult> GenerateTest()
        {
            await this._generateService.GenerateTestData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-question-data")]
        public async Task<IActionResult> GenerateQuestion()
        {
            await this._generateService.GenerateQuestionsData();
            return Ok();
        }

        [HttpPost]
        [Route("generate-stream-data")]
        public async Task<IActionResult> GenerateStream()
        {
            await this._generateService.GenerateStreamData();
            return Ok();
        }


    }
}
