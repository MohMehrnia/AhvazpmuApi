using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Dtos
{
    public class NewsDetailDto
    {
        public Guid tbNewsID { get; set; }
        public string fldNewsTitle { get; set; }
        public string fldNewsDescription { get; set; }
    }
}
