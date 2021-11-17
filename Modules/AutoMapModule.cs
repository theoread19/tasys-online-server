using AutoMapper;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Globalization;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Table;
using TASysOnlineProject.Table.Identity;

namespace TASysOnlineProject.Modules
{
    public class AutoMapModule : Profile
    {
        public AutoMapModule()
        {
            CreateMap<UserAccountRequest, UserAccountTable>();
            CreateMap<UserAccountTable, UserAccountResponse>().ReverseMap();
            CreateMap<UserInfoRequest, UserInfoTable>();
            CreateMap<UserInfoTable, UserInfoResponse>();
            CreateMap<CourseRequest, CourseTable>();
            CreateMap<CourseTable, CourseResponse>();
            CreateMap<SubjectRequest, SubjectTable>();
            CreateMap<SubjectTable, SubjectResponse>();
            CreateMap<UserAccountTable, IdentityUserAccount>()
                .ForMember(identity => identity.Id, op => op.MapFrom(userTable => userTable.Id))
                .ForMember(identity => identity.UserName, op => op.MapFrom(userTable => userTable.Username))
                .ForMember(identity => identity.PasswordHash, op => op.MapFrom(userTable => userTable.Password));
            CreateMap<PostRequest, PostTable>();
            CreateMap<PostTable, PostResponse>()
                .ForMember(m => m.UserAccountResponse, op => op.MapFrom(res => res.UserAccount))
                .ForMember(m => m.postLikeResponses, op => op.MapFrom(res => res.PostLikes))
                .ForMember(m => m.CountPostLike, op => op.MapFrom(res => res.PostLikes.Count));
            CreateMap<LessonRequest, LessonTable>();
            CreateMap<LessonTable, LessonResponse>();
            CreateMap<TestRequest, TestTable>();
            CreateMap<TestTable, TestResponse>()
                .ForMember(container => container.QuestionResponses, op => op.MapFrom(res => res.Questions));
            CreateMap<StreamSessionRequest, StreamSessionTable>();
            CreateMap<StreamSessionTable, StreamSessionResponse>()
                .ForMember(m => m.CourseTable, op => op.MapFrom(res => res.CourseTable))
                .ForMember(m => m.Creator, op => op.MapFrom(res => res.Creator));
            CreateMap<CartRequest, CartTable>();
            CreateMap<CartTable, CartResponse>()
                .ForMember(m => m.Courses, op => op.MapFrom(res => res.Courses));
            CreateMap<MessageRequest, MessageTable>();
            CreateMap<MessageTable, MessageResponse>()
                .ForMember(m => m.Sender, op => op.MapFrom(res => res.Sender));
            CreateMap<CommentRequest, CommentTable>();
            CreateMap<CommentTable, CommentResponse>()
                .ForMember(m => m.UserAccountResponse, op => op.MapFrom(res => res.UserAccount));
            CreateMap<QuestionRequest, QuestionTable>();
            CreateMap<QuestionTable, QuestionResponse>()
                .ForMember(container => container.AnswerResponses, op => op.MapFrom(res => res.Answers));
            CreateMap<QuestionRequest, QuestionResponse>();
            CreateMap<AnswerRequest, AnswerTable>();
            CreateMap<AnswerTable, AnswerResponse>();
            CreateMap<CloudBlobContainer, ContainerResponse>()
                .ForMember(container => container.Name, op => op.MapFrom(res => res.Name))
                .ForMember(container => container.Properties, op => op.MapFrom(res => res.Properties));
            CreateMap<MediaRequest, MediaTable>();
            CreateMap<MediaTable, MediaResponse>();
            CreateMap<MediaRequest, BlobContainerRequest>()
                .ForMember(m => m.FileName, op => op.MapFrom(res => res.FileName))
                .ForMember(m => m.FileContain, op => op.MapFrom(res => res.data))
                .ForMember(m => m.FileType, op => op.MapFrom(res => res.FileType))
                .ForMember(m => m.FileDirectory, op => op.MapFrom(res => res.Container));
            CreateMap<BillRequest, BillTable>();
            CreateMap<BillTable, BillResponse>()
                .ForMember(m => m.Courses, op => op.MapFrom(res => res.CourseTables));
            CreateMap<TestResultRequest, TestResultTable>();
            CreateMap<TestResultTable, TestResultResponse>()
                .ForMember(m => m.UserAccountResponse, op => op.MapFrom(res => res.UserAccount));
            CreateMap<PostLikeRequest, PostLikeTable>();
            CreateMap<PostLikeTable, PostLikeResponse>()
                .ForMember(m => m.UserAccountResponse, op => op.MapFrom(res => res.UserAccount));
        }
    }
}
