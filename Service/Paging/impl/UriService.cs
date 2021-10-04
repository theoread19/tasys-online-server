using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;
using TASysOnlineProject.Data.Requests;

namespace TASysOnlineProject.Service.Paging.impl
{
    public class UriService : IUriService
    {

        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetPageUri(Pagination filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "sortBy", filter.SortBy!.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "order", filter.Order!.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetPageUri(Search search, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", search.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", search.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "property", search.Property!.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "value", search.Value!.ToString());
            return new Uri(modifiedUri);
        }

        public Uri GetPageUri(Filter filter, string route)
        {
            var _enpointUri = new Uri(string.Concat(_baseUri, route));
            var modifiedUri = QueryHelpers.AddQueryString(_enpointUri.ToString(), "pageNumber", filter.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", filter.PageSize.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "property", filter.Property!.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "value", filter.Value!.ToString());
            return new Uri(modifiedUri);
        }
    }
}
