using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaBazaar.Data.Data.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        private new readonly ApplicationDbContext context;

        public BrandRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Brand> Search(string search)
        {
            search = search.ToLower();
            return context.Brands.Where(x => x.Name.Contains(search) || x.Description.Contains(search));
        }

        public void Update(Brand brand)
        {
            var brandDb = context.Brands.FirstOrDefault(x => x.Id == brand.Id);

            brandDb.Name = brand.Name;
            brandDb.Description = brand.Description;

            context.SaveChanges();
        }
    }
}
