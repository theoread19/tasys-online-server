using Autofac;
using TASysOnlineProject.Data.Provider;
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
            builder.RegisterType<UserAccountRepository>().As<IUserAccountRepository>().InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>().InstancePerLifetimeScope();
            builder.RegisterType<SubjectRepository>().As<ISubjectRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CourseRepository>().As<ICourseRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<LessonRepository>().As<ILessonRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostRepository>().As<IPostRepository>().InstancePerLifetimeScope();
            builder.RegisterType<StreamSessionRepository>().As<IStreamSessionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TestRepository>().As<ITestRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CartRepository>().As<ICartRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AnswerRepository>().As<IAnswerRepository>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionRepository>().As<IQuestionRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MessageRepository>().As<IMessageRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MediaRepository>().As<IMediaRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BillRepository>().As<IBillRepository>().InstancePerLifetimeScope();
            builder.RegisterType<TestResultRepository>().As<ITestResultRepository>().InstancePerLifetimeScope();
            builder.RegisterType<PostLikeRepository>().As<IPostLikeRepository>().InstancePerLifetimeScope();

            //service
            builder.RegisterType<UserAccountService>().As<IUserAccountService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<SubjectService>().As<ISubjectService>().InstancePerLifetimeScope();
            builder.RegisterType<CourseService>().As<ICourseService>().InstancePerLifetimeScope();
            builder.RegisterType<UserInfoService>().As<IUserInfoService>().InstancePerLifetimeScope();
            builder.RegisterType<IdentityService>().As<IIdentityService>().InstancePerLifetimeScope();
            builder.RegisterType<LessonService>().As<ILessonService>().InstancePerLifetimeScope();
            builder.RegisterType<PostService>().As<IPostService>().InstancePerLifetimeScope();
            builder.RegisterType<StreamSessionService>().As<IStreamSessionService>().InstancePerLifetimeScope();
            builder.RegisterType<TestService>().As<ITestService>().InstancePerLifetimeScope();
            builder.RegisterType<CartService>().As<ICartService>().InstancePerLifetimeScope();
            builder.RegisterType<MessageService>().As<IMessageService>().InstancePerLifetimeScope();
            builder.RegisterType<CommentService>().As<ICommentService>().InstancePerLifetimeScope();
            builder.RegisterType<QuestionService>().As<IQuestionService>().InstancePerLifetimeScope();
            builder.RegisterType<AnswerService>().As<IAnswerService>().InstancePerLifetimeScope();
            builder.RegisterType<ContainerService>().As<IContainerService>().InstancePerLifetimeScope();
            builder.RegisterType<MediaService>().As<IMediaService>().InstancePerLifetimeScope();
            builder.RegisterType<BlobService>().As<IBlobService>().InstancePerLifetimeScope();
            builder.RegisterType<BillService>().As<IBillService>().InstancePerLifetimeScope();
            builder.RegisterType<TestResultService>().As<ITestResultService>().InstancePerLifetimeScope();
            builder.RegisterType<StatisticService>().As<IStatisticService>().InstancePerLifetimeScope();
            builder.RegisterType<GenerateService>().As<IGenerateService>().InstancePerLifetimeScope();
            builder.RegisterType<PostLikeService>().As<IPostLikeService>().InstancePerLifetimeScope();
        }
    }
}
