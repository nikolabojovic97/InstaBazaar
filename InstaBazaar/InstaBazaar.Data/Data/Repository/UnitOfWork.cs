using InstaBazaar.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Data.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public IBrandRepository Brand { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IInstagramAccountRepository InstagramAccount { get; private set; }
        public UnitOfWork(ApplicationDbContext context, IBrandRepository Brand, ICategoryRepository Category, IInstagramAccountRepository InstagramAccount)
        {
            this.context = context;
            this.Category = Category;
            this.Brand = Brand;
            this.InstagramAccount = InstagramAccount;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        { 
            context.Dispose();
        }
    }
}
