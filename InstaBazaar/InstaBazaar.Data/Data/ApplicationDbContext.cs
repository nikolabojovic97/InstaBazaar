using System;
using System.Collections.Generic;
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
            builder.Entity<IdentityRole>().HasData(new IdentityRole(UserRolesConf.Admin) { NormalizedName = UserRolesConf.Admin.ToUpper()});
            builder.Entity<IdentityRole>().HasData(new IdentityRole(UserRolesConf.Brand) { NormalizedName = UserRolesConf.Brand.ToUpper() });
            builder.Entity<IdentityRole>().HasData(new IdentityRole(UserRolesConf.Influencer) { NormalizedName = UserRolesConf.Influencer.ToUpper() });
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InstagramAccount> InstagramAccounts { get; set; }
    }
}
