using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Dtos
{
    public class NewsImageDto
    {
        public Guid tbNewsID { get; set; }
        public byte[] fldSmallImage { get; set; }
        public byte[] fldLargImage { get; set; }
    }
}
