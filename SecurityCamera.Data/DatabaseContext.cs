using Microsoft.EntityFrameworkCore;
using SecurityCamera.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SecurityCamera.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<About> About { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Galery> Galeries { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Price> Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // local server
            //  optionsBuilder.UseSqlServer(@"Server=B24VELATBARAN\BT; Database=EticaretDb; Trusted_Connection=True; TrustServerCertificate=True;");

            // free server
            //optionsBuilder.UseSqlServer(@"workstation id=EticaretEgitimDb.mssql.somee.com;packet size=4096;user id=velatbaran_SQLLogin_4;pwd=g3mqgu6cjj;data source=EticaretEgitimDb.mssql.somee.com;persist security info=False;initial catalog=EticaretEgitimDb;TrustServerCertificate=True");
            //   optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)); // PendingModelChangesWarning hatası alınmaması için kullanılır
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // çalışan dll içinden configuration class ları bul
            base.OnModelCreating(modelBuilder);
        }
    }
}
