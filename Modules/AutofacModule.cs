using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Repository.TASysOnline.impl;
using TASysOnlineProject.Service.AzureStorage;
using TASysOnlineProject.Service.AzureStorage.impl;
using TASysOnlineProject.Service.TASysOnline;
using TASysOnlineProject.Service.TASysOnline.impl;

namespace TASysOnlineProject.Modules
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //repository
            builder.RegisterType<UserAccountRepository>().As<IUserAccountRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<SubjectRepository>().As<ISubjectRepository>();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>();
            builder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>();
            builder.RegisterType<DiscountRepository>().As<IDiscountRepository>();
            builder.RegisterType<LessonRepository>().As<ILessonRepository>();
            builder.RegisterType<PostRepository>().As<IPostRepository>();
            builder.RegisterType<ScheduleRepository>().As<IScheduleRepository>();
            builder.RegisterType<StreamSessionRepository>().As<IStreamSessionRepository>();
            builder.RegisterType<TestRepository>().As<ITestRepository>();
            builder.RegisterType<CurriCulumRepository>().As<ICurriCulumRepository>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();
            builder.RegisterType<AnswerRepository>().As<IAnswerRepository>();
            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>();
            builder.RegisterType<MediaRepository>().As<IMediaRepository>();
            builder.RegisterType<BillRepository>().As<IBillRepository>();
            builder.RegisterType<TestResultRepository>().As<ITestResultRepository>();

            //service
            builder.RegisterType<UserAccountService>().As<IUserAccountService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<AuthorService>().As<IAuthorService>();
            builder.RegisterType<SubjectService>().As<ISubjectService>();
            builder.RegisterType<CourseService>().As<ICourseService>();
            builder.RegisterType<UserInfoService>().As<IUserInfoService>();
            builder.RegisterType<IdentityService>().As<IIdentityService>();
            builder.RegisterType<DiscountService>().As<IDiscountService>();
            builder.RegisterType<LessonService>().As<ILessonService>();
            builder.RegisterType<PostService>().As<IPostService>();
            builder.RegisterType<ScheduleService>().As<IScheduleService>();
            builder.RegisterType<StreamSessionService>().As<IStreamSessionService>();
            builder.RegisterType<TestService>().As<ITestService>();
            builder.RegisterType<CurriCulumService>().As<ICurriCulumService>();
            builder.RegisterType<CartService>().As<ICartService>();
            builder.RegisterType<MessageService>().As<IMessageService>();
            builder.RegisterType<CommentService>().As<ICommentService>();
            builder.RegisterType<QuestionService>().As<IQuestionService>();
            builder.RegisterType<AnswerService>().As<IAnswerService>();
            builder.RegisterType<ContainerService>().As<IContainerService>();
            builder.RegisterType<MediaService>().As<IMediaService>();
            builder.RegisterType<BlobService>().As<IBlobService>();
            builder.RegisterType<BillService>().As<IBillService>();
            builder.RegisterType<TestResultService>().As<ITestResultService>();
            builder.RegisterType<StatisticService>().As<IStatisticService>();
        }
    }
}
