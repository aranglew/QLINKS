using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace QFSLINKS.Data
{
    public class QfslinksContext : DbContext
    {
        public QfslinksContext(DbContextOptions<QfslinksContext> options)
            : base(options)
        { }

        public DbSet<Data2> Links { get; set; }
        public DbSet<Datau2> Users { get; set; }
    }

    public class Data2 {
        public int TID { get; set; }

        public int GroupID { get; set; }
        public int TopicID { get; set; }
        public string Topic { get; set; }
        public string Type { get; set; }
        public decimal SortOrder { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
        public string Location { get; set; }
        public string Format { get; set; }
        public string Detail { get; set; }
    }
    public class Datau2
    {
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
    }
}
