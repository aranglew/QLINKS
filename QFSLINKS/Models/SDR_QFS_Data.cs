using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QFSLINKS.Models
{
    public class SDR_QFS_Data
    {  [Key]
        public int TID { get; set; }

        public int GroupID { get; set; }
        public int TopicID { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0}")]
        public decimal? SortOrder { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string Location { get; set; }
        public string Format { get; set; }
        public string Detail { get; set; }
    }
}