using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AhvazpmuPortable.Model
{
    public class News
    {
        public Guid tbNewsID { get; set; }
        public Guid tbNewsGroupID { get; set; }
        public Guid tbUserID { get; set; }
        public string fldNewsTitle { get; set; }
        public string fldNewsDate { get; set; }
        public string fldRegisterDate { get; set; }
        public long? fldNewsVisited { get; set; }
        public string fldSummaryNews { get; set; }
        public string fldRegisterTime { get; set; }
        public string fldNewsExternalLink { get; set; }
        public string ImageUrl
        {
            get
            {
                return "http://api.ahvazpmu.ir/newsImage/" + tbNewsID.ToString();
            }
        }
        public string LongDate { get; set; }

    }
}
