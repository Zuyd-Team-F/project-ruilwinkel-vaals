using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuilwinkelVaals.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<BusinessData> BusinessData { get; set; }
        public DbSet<ProductLog> ProductLogs { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<LoanedProduct> LoanedProducts { get; set; }
        public DbSet<Blacklist> Blacklist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "UserData");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");    
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<BusinessData>()
            .HasIndex(u => u.Name)
            .IsUnique();

            builder.Entity<BusinessData>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Status>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Category>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<Condition>()
                .HasIndex(u => u.Name)
                .IsUnique();
        }
    }
}
