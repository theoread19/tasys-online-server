using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Repository.TASysOnline;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Table;
using TASysOnlineProject.Utils;

namespace TASysOnlineProject.Service.TASysOnline.impl
{
    public class SubjectService : ISubjectService
    {
        public ISubjectRepository _subjectRepository { get; set; }

        private IUriService _uriService;

        private IMapper _mapper;
        public SubjectService( ISubjectRepository subjectRepository, IUriService uriService, IMapper mapper)
        {
            this._subjectRepository = subjectRepository;
            this._uriService = uriService;
            this._mapper = mapper;
        }

        public async Task<Response> CreateSubjectAsync(SubjectRequest subjectRequest)
        {
            if (subjectRequest == null || subjectRequest.Name == null || subjectRequest.Name.Length <= 0)
            {
                return new Response
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ResponseMessage = "Null Exception!"
                };
            }

            var result = await this.FindByNameAsync(subjectRequest.Name);
            if (result.StatusCode == StatusCodes.Status200OK)
            {
                return new Response
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    ResponseMessage = "Subject are exist!"
                };
            }

            var table = this._mapper.Map<SubjectTable>(subjectRequest);
            table.CreatedDate = DateTime.UtcNow;

            await this._subjectRepository.InsertAsync(table);
            await this._subjectRepository.SaveAsync();

            return new Response { StatusCode = StatusCodes.Status201Created, ResponseMessage = "Created Subject Successfully!" };
        }

        public async Task<SubjectResponse> FindByNameAsync(string subjectName)
        {
            var result = await this._subjectRepository.FindByNameAsync(subjectName);

            if (result == null)
            {
                return new SubjectResponse 
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ResponseMessage = "Subject not found!"
                };
            }

            var response = this._mapper.Map<SubjectResponse>(result);
            response.ResponseMessage = "Subject is found!";
            response.StatusCode = StatusCodes.Status200OK;

            return response;
        }

        public async Task<IEnumerable<SubjectResponse>> GetAllSubjectAsync()
        {
            var tables = await this._subjectRepository.GetAllAsync();

            List<SubjectResponse> respones = this._mapper.Map<List<SubjectTable>, List<SubjectResponse>>(tables);

            return respones;
        }

        public async Task<PageResponse<List<SubjectResponse>>> GetAllSubjectPagingAsync(Pagination paginationFilter, string route)
        {
            var validFilter = new Pagination(paginationFilter.PageNumber, paginationFilter.PageSize, paginationFilter.SortBy!, paginationFilter.Order!);
            var totalData = await this._subjectRepository.CountAsync();

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<SubjectResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "No data!";
                return reponse;
            }

            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._subjectRepository.GetAllPadingAsync(validFilter);

            if( tables == null)
            {
                var reponse = PaginationHelper.CreatePagedReponse<SubjectResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status500InternalServerError;
                reponse.ResponseMessage = "Column name inlvaid";
                return reponse;
            }

            var pageData = this._mapper.Map<List<SubjectTable>, List<SubjectResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<SubjectResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<SearchResponse<List<SubjectResponse>>> SearchSubjectBy(Search searchRequest, string route)
        {
            var validFilter = new Search(searchRequest.PageNumber, searchRequest.PageSize, searchRequest.SortBy!, searchRequest.Order!, searchRequest.Value!, searchRequest.Property!);

            var totalData = await this._subjectRepository.CountByAsync(validFilter.Property!, validFilter.Value!);

            if (totalData == 0)
            {
                var reponse = PaginationHelper.CreatePagedReponse<SubjectResponse>(null, validFilter, totalData, this._uriService, route);
                reponse.StatusCode = StatusCodes.Status404NotFound;
                reponse.ResponseMessage = "Not Found";
                return reponse;
            }


            validFilter.PageSize = (totalData < validFilter.PageSize) ? totalData : validFilter.PageSize;

            var tables = await this._subjectRepository.SearchByAsync(validFilter);

            var pageData = this._mapper.Map<List<SubjectTable>, List<SubjectResponse>>(tables);

            var pagedReponse = PaginationHelper.CreatePagedReponse<SubjectResponse>(pageData, validFilter, totalData, this._uriService, route);
            pagedReponse.StatusCode = StatusCodes.Status200OK;
            pagedReponse.ResponseMessage = "Fectching data successfully!";
            return pagedReponse;
        }

        public async Task<Response> UpdateSubject(SubjectRequest subject)
        {
            var table = await this._subjectRepository.FindByIdAsync(subject.Id);
            table.Name = subject.Name;
            table.CreatedDate = DateTime.UtcNow;

            await this._subjectRepository.UpdateAsync(table);

            await this._subjectRepository.SaveAsync();

            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Update Subject Successfully!"
            };
        }

        public async Task<Response> DeleteSubject(Guid[] subjectId)
        {
            for(var i = 0; i < subjectId.Length; i++)
            {
                await this._subjectRepository.DeleteAsync(subjectId[i]);
            }

            await this._subjectRepository.SaveAsync();
            return new Response
            {
                StatusCode = StatusCodes.Status200OK,
                ResponseMessage = "Delete Subject Successfully!"
            };
        }
    }
}
