using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace InstaBazaar.Models
{
    public class InstagramAccount
    {
        public int Id { get; set; }
        public string IgUserName { get; set; }
        public int IgId { get; set; }
        public string Description { get; set; }
        public DateTime MemberSince { get; set; }
        public int TotalFollowers { get; set; }
        public int TotalPosts { get; set; }
        public double AvgComments { get; set; }
        public double AvgLikes { get; set; }

        public List<Service> Services { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
