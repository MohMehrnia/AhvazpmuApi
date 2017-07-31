using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AhvazpmuApi.Dtos
{
    public class NewsDto
    {
        public Guid tbNewsID { get; set; }

        [Required]
        public Guid tbNewsGroupID { get; set; }

        [Required]
        public Guid tbUserID { get; set; }

        [Required]
        [MaxLength(1000)]
        public string fldNewsTitle { get; set; }

        public string fldNewsDate { get; set; }
        public string fldRegisterDate { get; set; }
        public long? fldNewsVisited { get; set; }
        public string fldSummaryNews { get; set; }
        public string fldRegisterTime { get; set; }
        public string fldNewsExternalLink { get; set; }

        public string LongDate
        {
            get
            {
                if (!String.IsNullOrEmpty(fldRegisterDate))
                {
                    string[] Da = fldRegisterDate.Split('/');
                    if (Da.Length > 2)
                    {
                        DateTime TempDate = new DateTime(Convert.ToInt32(Da[2]), Convert.ToInt32(Da[0]), Convert.ToInt32(Da[1]));
                        PersianCalendar jc = new PersianCalendar();
                        return string.Format("{0:0000}/{1:00}/{2:00}", jc.GetYear(TempDate), jc.GetMonth(TempDate), jc.GetDayOfMonth(TempDate));
                    }
                    else
                    {
                        return "";
                    }


                }
                else
                {
                    return "";
                }
            }
        }
    }
}
