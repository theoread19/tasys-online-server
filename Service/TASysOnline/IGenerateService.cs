using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TASysOnlineProject.Service.TASysOnline
{
    public interface IGenerateService
    {
        public Task GenerateRoleData();

        public Task GenerateUserAccountData();

        public Task GenerateCourseData();

        public Task GenerateSubjectData();

        public Task GenerateTestData();

        public Task GenerateQuestionsData();

        public Task GenerateCurriCulumData();

        public Task GenerateLessonData();

        public Task GenerateScheduleData();

        public Task GenerateStreamData();
    }
}
