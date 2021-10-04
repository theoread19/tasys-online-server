using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TASysOnlineProject.Data;

namespace TASysOnlineProject.Service.Paging
{
    public interface IUriService
    {
        public Uri GetPageUri(Pagination filter, string route);
    }
}
