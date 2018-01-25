using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QFSLINKS.Models
{
    public class DTO_menubyuser
    {
        public int TopicId { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public string Data { get; set; }

        public string Format { get; set; }

        public string Detail { get; set; }

        public int TopicUserID { get; set; }

        public Decimal? SortOrder { get; set; }

        public string DataU { get; set; }

        public int? VimsVisible { get; set; }

        public string Username { get; set; }
    }
}
