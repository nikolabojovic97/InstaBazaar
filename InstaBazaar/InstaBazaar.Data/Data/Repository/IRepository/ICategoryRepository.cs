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
        Task<IEnumerable<Category>> SearchAsync(string search);
        Task UpdateAsync(Category category);
        Task<string> SaveImageAsync(IFormFile file, string oldImagePath = null);
        IEnumerable<Category> Search(IEnumerable<Category> categories, string search);
    }
}
