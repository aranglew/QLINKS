using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace QFSLINKS.Models
{
    public class SDR_QFS_Division
    {
        [Key]
        public int ID { get; set; }

        public string DivisionName { get; set; }
    }
}
