using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace QFSLINKS.Models
{
    public class SDR_QFS_LinkType
    {
        [Key]
        public int Id { get; set; }
        public string LinkType { get; set; }
    }
}
