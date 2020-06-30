using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Models.ViewModels
{
    public class CategoryViewModel
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public PagingInfoViewModel PagingInfoViewModel { get; set; }
        public string Search { get; set; }
        public List<Category> Categories { get; set; }
    }
}
