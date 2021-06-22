using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RuilwinkelVaals.WebApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RuilwinkelVaals.WebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserData> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<BusinessData> BusinessDatas { get; set; }
        public DbSet<ProductLog> ProductLogs { get; set; }
        public DbSet<Remark> Remarks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<LoanedProduct> LoanedProducts { get; set; }
        public DbSet<Blacklist> Blacklist { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserData>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<BusinessData>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<BusinessData>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<Role>()
                .HasIndex(u => u.Name)
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
