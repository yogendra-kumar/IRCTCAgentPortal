using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Mpower.Data.Models;

namespace Mpower.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private const Int64 defaultValue = 0;
        public DbSet<Application> ObjApplication { get; set; }
        public DbSet<Application_Layouts> ObjApplication_Layouts { get; set; }
        public DbSet<Application_Settings> ObjApplication_Settings { get; set; }
        public DbSet<Application_Errors> ObjApplicationErrors { get; set; }
        public DbSet<Application_ResponseCodes> ObjApplicationResponseCodes { get; set; }
        public DbSet<Application_PagePans> ObjPage_Pans { get; set; }
        public DbSet<Application_Pages> ObjPages { get; set; }
        
        public DbSet<Application_Files> ObjApplicationFiles{get;set;}

        public DbSet<Pages_Metadata> objPagesMetadata{get;set;}
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entity in builder.Model.GetEntityTypes())
            {
                entity.Relational().TableName = entity.DisplayName();
            }
            builder.Entity<Application_Pages>().Property(b=>b.leftPanId).HasDefaultValue(defaultValue);
            builder.Entity<Application_Pages>().Property(b=>b.rightPanId).HasDefaultValue(defaultValue);
            builder.Entity<Application>().Property(b=>b.footerId).HasDefaultValue(defaultValue);
            builder.Entity<Application_Pages>().Property(b=>b.metaId).HasDefaultValue(defaultValue);
            base.OnModelCreating(builder);
        }
    }
}
