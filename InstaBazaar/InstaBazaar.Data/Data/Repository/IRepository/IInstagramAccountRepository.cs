using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IInstagramAccountRepository : IRepository<InstagramAccount>
    {
        IEnumerable<InstagramAccount> GetByCategory(Category category);
        IEnumerable<InstagramAccount> GetByCategory(IEnumerable<InstagramAccount> accounts, Category category);
        IEnumerable<InstagramAccount> GetByCategories(IEnumerable<InstagramAccount> accounts, IEnumerable<Category> categories);
        IEnumerable<InstagramAccount> Search(string search);
        IEnumerable<InstagramAccount> Search(IEnumerable<InstagramAccount> accounts, string search);
        void Update(InstagramAccount account);
        void UpdateAccountCategory(InstagramAccount account, Category category);
    }
}
