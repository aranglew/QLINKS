using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QFSLINKS.Models
{
    public class DTO_MenuItem
    {
        public int TopicID { get; set; }

        public string Topic { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N0}")]
        public int? sortOrder { get; set; }

        public string Location { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Data is required")]
        public string Data { get; set; }

        public string Format { get; set; }

        public string Detail { get; set; }
    }
}
