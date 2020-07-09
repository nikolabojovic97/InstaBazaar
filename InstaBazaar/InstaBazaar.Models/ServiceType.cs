using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Models
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }

        public List<Service> Services { get; set; }
    }
}
