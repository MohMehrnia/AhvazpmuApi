using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AhvazpmuApi.Entities
{
    [Table("tbNewsImage")]
    public class NewsImage
    {
        public Guid tbNewsID { get; set; }
        public byte[] fldSmallImage { get; set; }
        public byte[] fldLargImage { get; set; }
        public virtual News News { get; set; }
    }
}
