using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QFSLINKS.Models
{
    public class SDR_QFS_Datau
    {
        [Key]
        public int TopicUserID { get; set; }
        public decimal SortOrder { get; set; }
        public int TopicID { get; set; }
        public string UserName { get; set; }
        public string Data { get; set; }
        public string Access { get; set; }
        public string Location { get; set; }
        public string AccessA { get; set; }
        public string Division { get; set; }
        public string UserInitials { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public int VimsVisible { get; set; }
        public int VimsDelegate { get; set; }
        public string VimsAccess { get; set; }


        public virtual ICollection<SDR_QFS_Data> Links { get; set; }
    }
}
