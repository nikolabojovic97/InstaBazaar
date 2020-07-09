using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IInstagramAccountRepository : IRepository<InstagramAccount>
    {
        IEnumerable<InstagramAccount> GetByCategory(Category category);
        IEnumerable<InstagramAccount> GetByCategory(IEnumerable<InstagramAccount> accounts, Category category);
        IEnumerable<InstagramAccount> GetByCategories(IEnumerable<InstagramAccount> accounts, IEnumerable<Category> categories);
        Task<IEnumerable<InstagramAccount>> SearchAsync(string search);
        IEnumerable<InstagramAccount> Search(IEnumerable<InstagramAccount> accounts, string search);
        Task UpdateAsync(InstagramAccount account);
        Task UpdateAccountCategoryAsync(InstagramAccount account, Category category);
    }
}
