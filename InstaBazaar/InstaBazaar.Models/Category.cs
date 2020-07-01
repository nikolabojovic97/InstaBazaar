using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace InstaBazaar.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Naziv")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        //public string ImageUrl { get; set; }

        public List<InstagramAccount> InstagramAccounts { get; set; }
    }
}
