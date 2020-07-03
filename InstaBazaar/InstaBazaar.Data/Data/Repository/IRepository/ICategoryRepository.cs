using InstaBazaar.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();
        IEnumerable<Category> Search(string search);
        void Update(Category category);
        Task<string> SaveImageAsync(IFormFile file, string oldImagePath = null);
    }
}
