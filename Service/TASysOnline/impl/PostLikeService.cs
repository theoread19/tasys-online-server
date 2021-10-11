using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;

        private readonly IMapper _mapper;

        public PostLikeService(IPostLikeRepository postLikeRepository, IMapper mapper)
        {
            this._postLikeRepository = postLikeRepository;
            this._mapper = mapper;
        }
        public async Task<Response> LikeOrUnlikePost(PostLikeRequest postLikeRequest)
        {
            var table = await this._postLikeRepository.FindPostLikeByPostIdAndUserId(postLikeRequest.PostId, postLikeRequest.UserAccountId);
            if (table != null){
                await this._postLikeRepository.DeleteAsync(table.Id);
                await this._postLikeRepository.SaveAsync();
                return new Response { StatusCode = StatusCodes.Status200OK, ResponseMessage = "Unlike post successfully!" };
            }

            table!.CreatedDate = DateTime.UtcNow;
            await this._postLikeRepository.InsertAsync(table);
            await this._postLikeRepository.SaveAsync();
            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Like post successfully!" };
        }
    }
}
