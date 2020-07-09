using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using InstaBazaar.Models;
using InstaBazaar.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InstaBazaar.Data.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole(UserRolesConf.Admin) { NormalizedName = UserRolesConf.Admin.ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole(UserRolesConf.User) { NormalizedName = UserRolesConf.User.ToUpper() });

            builder.Entity<ServiceType>().HasData(new ServiceType() { Id = 1, Name = "Stori 24", OrderNumber = 1 });
            builder.Entity<ServiceType>().HasData(new ServiceType() { Id = 2, Name = "Stori 24h + link", OrderNumber = 2 });
            builder.Entity<ServiceType>().HasData(new ServiceType() { Id = 3, Name = "Post 24h", OrderNumber = 3 });
            builder.Entity<ServiceType>().HasData(new ServiceType() { Id = 4, Name = "Post 24h + link", OrderNumber = 4 });
            builder.Entity<ServiceType>().HasData(new ServiceType() { Id = 5, Name = "Trajni post", OrderNumber = 5 });
            builder.Entity<ServiceType>().HasData(new ServiceType() { Id = 6, Name = "Trajni post + link", OrderNumber = 6 });

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<InstagramAccount> InstagramAccounts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
    }
}
