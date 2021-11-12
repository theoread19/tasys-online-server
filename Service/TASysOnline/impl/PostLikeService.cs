using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Table;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;

        private IMapper _mapper;

        private readonly IPostService _postService;

        private readonly IUserAccountService _userAccountService;

        public PostLikeService(IPostLikeRepository postLikeRepository, IMapper mapper, IPostService postService, IUserAccountService userAccountService)
        {
            this._postLikeRepository = postLikeRepository;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._postService = postService;
        }

        public async Task<Response> LikeOrUnlikePost(PostLikeRequest postLikeRequest)
        {
            var user = await this._userAccountService.FindByIdAsync(postLikeRequest.UserAccountId);

            if (user.StatusCode == StatusCodes.Status404NotFound)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "User not found!" };
            }

            var post = await this._postService.GetPostById(postLikeRequest.PostId);

            if (post.StatusCode == StatusCodes.Status404NotFound)
            {
                return new Response { StatusCode = StatusCodes.Status404NotFound, ResponseMessage = "Post not found!" };
            }

            var table = await this._postLikeRepository.FindPostLikeByPostIdAndUserId(postLikeRequest.PostId, postLikeRequest.UserAccountId);

            if (table != null){
                await this._postLikeRepository.DeleteAsync(table.Id);
                await this._postLikeRepository.SaveAsync();
                return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Unlike post successfully!" };
            }

            var postLike = this._mapper.Map<PostLikeTable>(postLikeRequest);
            postLike.CreatedDate = DateTime.UtcNow;
            await this._postLikeRepository.InsertAsync(postLike);
            await this._postLikeRepository.SaveAsync();
            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Like post successfully!" };
        }
    }
}
