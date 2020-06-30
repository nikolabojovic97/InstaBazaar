using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Models
{
    public enum UserType
    {
        Brand, Influencer
    }

    public class User : IdentityUser
    {
        public UserType UserType { get; set; }
        public List<InstagramAccount> InstagramAccounts { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
