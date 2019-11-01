using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Entities.Requests.Queries
{
    public class PaginationQuery
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PaginationQuery(int pageNumber = 1, int pageSize = 100)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
