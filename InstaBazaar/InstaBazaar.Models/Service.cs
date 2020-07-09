using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaBazaar.Models
{
    public class Service
    {
        public int Id { get; set; }
        public double Price { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
        public int InstagramAccountId { get; set; }
        public InstagramAccount InstagramAccount { get; set; }
    }
}
