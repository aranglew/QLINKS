using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using QFSLINKS.Models;

namespace QFSLINKS.Data
{
    public class QfslinksContext : DbContext
    {
        public QfslinksContext(DbContextOptions<QfslinksContext> options)
            : base(options)
        { }

        public DbSet<SDR_QFS_Data> SDR_QFS_Data { get; set; }
        public DbSet<SDR_QFS_Datau> SDR_QFS_DataU { get; set; }
        public DbSet<SDR_QFS_Division> SDR_QFS_Division { get; set; }
        public DbSet<SDR_QFS_LinkType> SDR_QFS_LinkType { get; set; }
        
    }
    
}
