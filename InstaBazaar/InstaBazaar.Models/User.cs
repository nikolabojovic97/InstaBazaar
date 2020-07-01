using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Models
{
    public class User : IdentityUser
    {
        public List<InstagramAccount> InstagramAccounts { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
