using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QFSLINKS.Models;
using System.ComponentModel.DataAnnotations;

namespace QFSLINKS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
            public DbSet<Datas> Links { get; set; }
            public DbSet<Datau> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


    }
    public class Datas
{
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
    public class Datau
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
}
}
