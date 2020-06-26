using InstaBazaar.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Data.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Category = new CategoryRepository(this.context);
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
