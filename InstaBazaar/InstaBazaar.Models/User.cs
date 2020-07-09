using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Models
{
    public class User : IdentityUser
    {
        public IdentityRole Role { get; set; }
        public List<InstagramAccount> InstagramAccounts { get; set; }
    }
}
