using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IInstagramAccountRepository : IRepository<InstagramAccount>
    {
        IEnumerable<InstagramAccount> Search(string search);
        void Update(InstagramAccount account);
        void UpdateAccountCategory(InstagramAccount account, Category category);
    }
}
