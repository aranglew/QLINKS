using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QFSLINKS.Models.MenuViewModels
{
    public class UserLinksData
    {
        public IEnumerable<SDR_QFS_Data> Users { get; set; }
        public IEnumerable<SDR_QFS_Datau> Links { get; set; }
    }
}
