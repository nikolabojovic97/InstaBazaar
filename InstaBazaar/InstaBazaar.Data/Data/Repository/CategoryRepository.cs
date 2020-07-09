using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using InstaBazaar.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace InstaBazaar.Data.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private new readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment hostEnvironment;

        public CategoryRepository(ApplicationDbContext context, IWebHostEnvironment hostEnvironment) : base(context)
        {
            this.context = context;
            this.hostEnvironment = hostEnvironment;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return context.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
        }

        public async Task<string> SaveImageAsync(IFormFile file, string oldImagePath = null)
        {
            return await Task.Run(async () =>
            {
                string webRootPath = hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString();
                var uploadPath = Path.Combine(webRootPath, UrlPaths.categoryFolderPath);
                var extension = Path.GetExtension(file.FileName);

                if (!string.IsNullOrEmpty(oldImagePath))
                    await DeleteImageAsync(oldImagePath);

                var fileStream = new FileStream(Path.Combine(uploadPath, fileName + extension), FileMode.Create);
                await file.CopyToAsync(fileStream);
                fileStream.Dispose();

                return @"\" + UrlPaths.categoryFolderPath + fileName + extension;
            });
        }

        public async Task<IEnumerable<Category>> SearchAsync(string search)
        {
            return await Task.Run(() =>
            {
                search = search.ToLower();
                return context.Categories.Where(x => x.Name.ToLower().Contains(search) || x.Description.ToLower().Contains(search)).ToList();
            });
        }

        public async Task UpdateAsync(Category category)
        {
            await Task.Run( async () =>
            {
                var categoryDb = await context.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

                categoryDb.Name = category.Name;
                categoryDb.Description = category.Description;
                categoryDb.ImageUrl = category.ImageUrl;
            });
        }

        private async Task DeleteImageAsync(string path)
        {
            await Task.Run(() =>
            {
                var deletePath = hostEnvironment.WebRootPath + path;
                if (System.IO.File.Exists(deletePath))
                    System.IO.File.Delete(deletePath);
            });     
        }

        public override async Task RemoveAsync(Category category)
        {
            await Task.Run( async () =>
            {
                await base.RemoveAsync(category);
                await DeleteImageAsync(category.ImageUrl);
            });
        }

        public IEnumerable<Category> Search(IEnumerable<Category> categories, string search)
        {
            search = search.ToLower();
            return categories.Where(x => x.Name.ToLower().Contains(search) || x.Description.ToLower().Contains(search)).ToList();
        }
    }
}
