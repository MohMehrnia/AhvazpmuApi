using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.QueryParameters
{
    public class NewsQueryParameters
    {
        private const int maxPageCount = 30;
        public int Page { get; set; } = 1;
        private int _pageCount = 10;

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = (value > maxPageCount) ? maxPageCount : value; }
        }
    }
}
