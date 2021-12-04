﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Const;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Controllers.TASysOnline
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;

        public MediaController(IMediaService mediaService)
        {
            this._mediaService = mediaService;
        }

        [HttpPost]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> CreateMedia([FromBody] MediaRequest[] mediaRequests)
        {
            var response = await this._mediaService.CreateMediasAsync(mediaRequests);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> GetAllMediaByContainerAsync(string container)
        {
            var responses = await this._mediaService.FindByContainerNameAsync(container);
            return StatusCode(StatusCodes.Status200OK, responses);
        }

        [HttpPost]
        [Route("delete")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Instructor)]
        public async Task<IActionResult> DeleteMediaAsync([FromBody] Guid[] mediaId)
        {
            var response = await this._mediaService.DeleteMediaAsync(mediaId);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("move")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Instructor)]
        public async Task<IActionResult> MoveMediaAsync([FromBody] MediasRequest mediaRequest)
        {
            var response = await this._mediaService.MoveMediasAsync(mediaRequest);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("change-name")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Instructor)]
        public async Task<IActionResult> ChangeMediaNameAsync([FromBody] MediaChangeNameRequest mediaRequest)
        {
            var response = await this._mediaService.ChangeMediaNameAsync(mediaRequest);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Instructor)]
        public async Task<IActionResult> UpdateMediaAsync([FromBody] UpdateMediaRequest mediaRequest)
        {
            var response = await this._mediaService.UpdateMediaAsync(mediaRequest);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut]
        [Route("copy")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Instructor)]
        public async Task<IActionResult> CopyMediaAsync([FromBody] MediasRequest mediaRequest)
        {
            var response = await this._mediaService.CopyMediasAsync(mediaRequest);
            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("download/file")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DownloadFile([FromBody] MediaDownRequest mediaRequest)
        {
            var response = await this._mediaService.FindMediaByIdAsync(mediaRequest.Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpPost]
        [Route("download/files")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> DownloadFileZip([FromBody] Guid[] mediaIds)
        {
            var zip = await this._mediaService.DownloadFileZipAsync(mediaIds);
            return File(zip, "application/octet-stream");
        }

        [HttpGet]
        [Route("filter")]
        [Authorize(Roles = Roles.All)]
        public async Task<IActionResult> FilterMedia([FromQuery] Filter filterRequest)
        {
            var route = Request.Path.Value;
            var responses = await this._mediaService.FilterMediaBy(filterRequest, route);
            return StatusCode(StatusCodes.Status200OK, responses);
        }
    }
}
