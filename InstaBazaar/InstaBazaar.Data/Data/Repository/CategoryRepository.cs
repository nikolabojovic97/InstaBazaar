﻿using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace InstaBazaar.Data.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return context.Categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var categoryDb = context.Categories.FirstOrDefault(x => x.Id == category.Id);

            categoryDb.Name = category.Name;
            categoryDb.Description = category.Description;

            context.SaveChanges();
        }
    }
}
