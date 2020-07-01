using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaBazaar.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Naziv")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
