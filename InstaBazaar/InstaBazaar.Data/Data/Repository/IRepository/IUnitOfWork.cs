using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IBrandRepository Brand { get; }
        IInstagramAccountRepository InstagramAccount { get; }
        void Save();
    }
}
