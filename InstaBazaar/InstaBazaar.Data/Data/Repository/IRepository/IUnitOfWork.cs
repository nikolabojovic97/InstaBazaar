using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IInstagramAccountRepository InstagramAccount { get; }
        IUserRepository User { get; set; }
        IServiceRepository Service { get; }
        IServiceTypeRepository ServiceType { get; }
        Task SaveAsync();
    }
}
