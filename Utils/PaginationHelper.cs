using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;
using TASysOnlineProject.Data.Responses;
using TASysOnlineProject.Service.Paging;
using TASysOnlineProject.Service.TASysOnline;

namespace TASysOnlineProject.Utils
{
    public class PaginationHelper
    {
        public static PageResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, Pagination validFilter, int totalRecords, IUriService uriService, string route)
        {
            if (pagedData == null)
            {
                return new PageResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize); ;
            }

            var respose = new PageResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new Pagination(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new Pagination(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new Pagination(1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!), route);
            respose.LastPage = uriService.GetPageUri(new Pagination(roundedTotalPages, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static SearchResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, Search validFilter, int totalRecords, IUriService uriService, string route)
        {
            if (pagedData == null)
            {
                return new SearchResponse<List<T>>(pagedData!, validFilter.PageNumber, validFilter.PageSize, validFilter.Value!, validFilter.Property!); ;
            }

            var respose = new SearchResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize, validFilter.Value!, validFilter.Property!);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new Search(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new Search(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new Search(1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route);
            respose.LastPage = uriService.GetPageUri(new Search(roundedTotalPages, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static FilterResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, Filter validFilter, int totalRecords, IUriService uriService, string route)
        {
            if (pagedData == null)
            {
                return new FilterResponse<List<T>>(pagedData!, validFilter.PageNumber, validFilter.PageSize, validFilter.Value!, validFilter.Property!); ;
            }

            var respose = new FilterResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize, validFilter.Value!, validFilter.Property!);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new Filter(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new Filter(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new Filter(1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route);
            respose.LastPage = uriService.GetPageUri(new Filter(roundedTotalPages, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.Value!, validFilter.Property!), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }

        public static FilterSearchResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, FilterSearch validFilter, int totalRecords, IUriService uriService, string route)
        {
            if (pagedData == null)
            {
                return new FilterSearchResponse<List<T>>(pagedData!, validFilter.PageNumber, validFilter.PageSize, validFilter.FilterValue!, validFilter.FilterProperty!, validFilter.SearchValue!, validFilter.SearchProperty!); ;
            }

            var respose = new FilterSearchResponse<List<T>>(pagedData, validFilter.PageNumber, validFilter.PageSize, validFilter.FilterValue!, validFilter.FilterProperty!, validFilter.SearchValue!, validFilter.SearchProperty!);
            var totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            respose.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new FilterSearch(validFilter.PageNumber + 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.FilterValue!, validFilter.FilterProperty!, validFilter.SearchValue!, validFilter.SearchProperty!), route)
                : null;
            respose.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new FilterSearch(validFilter.PageNumber - 1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.FilterValue!, validFilter.FilterProperty!, validFilter.SearchValue!, validFilter.SearchProperty!), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new FilterSearch(1, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.FilterValue!, validFilter.FilterProperty!, validFilter.SearchValue!, validFilter.SearchProperty!), route);
            respose.LastPage = uriService.GetPageUri(new FilterSearch(roundedTotalPages, validFilter.PageSize, validFilter.SortBy!, validFilter.Order!, validFilter.FilterValue!, validFilter.FilterProperty!, validFilter.SearchValue!, validFilter.SearchProperty!), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
