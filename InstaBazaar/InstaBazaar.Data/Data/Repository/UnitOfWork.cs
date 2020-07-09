using InstaBazaar.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public ICategoryRepository Category { get; private set; }
        public IInstagramAccountRepository InstagramAccount { get; private set; }
        public IUserRepository User { get; set; }
        public IServiceRepository Service { get; private set; }
        public IServiceTypeRepository ServiceType { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ICategoryRepository Category, IInstagramAccountRepository InstagramAccount,
            IUserRepository User, IServiceRepository Service, IServiceTypeRepository ServiceType)
        {
            this.context = context;
            this.Category = Category;
            this.InstagramAccount = InstagramAccount;
            this.User = User;
            this.Service = Service;
            this.ServiceType = ServiceType;
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        { 
            context.Dispose();
        }
    }
}
