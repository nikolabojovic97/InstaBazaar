using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Models.ViewModels
{
    public class ViewModel<T> where T : class
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public PagingInfoViewModel PagingInfoViewModel { get; set; }
        public string Search { get; set; }
        public List<T> List { get; set; }
    }
}
