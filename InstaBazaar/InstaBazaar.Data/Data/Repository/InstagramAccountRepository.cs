using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InstaBazaar.Data.Data.Repository
{
    public class InstagramAccountRepository : Repository<InstagramAccount>, IInstagramAccountRepository
    {
        private new readonly ApplicationDbContext context;

        public InstagramAccountRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<InstagramAccount> GetByCategory(Category category)
        {
            var accounts = context.InstagramAccounts;
            return GetByCategory(accounts, category);
        }

        public IEnumerable<InstagramAccount> GetByCategory(IEnumerable<InstagramAccount> accounts, Category category)
        {
            foreach (var account in accounts)
                if (account.CategoryId == category.Id)
                    yield return account;
        }

        public IEnumerable<InstagramAccount> GetByCategories(IEnumerable<InstagramAccount> accounts, IEnumerable<Category> categories)
        {
            foreach (var category in categories)
                yield return (InstagramAccount)GetByCategory(accounts, category);
        }

        public IEnumerable<InstagramAccount> Search(string search)
        {
            var accounts = context.InstagramAccounts;
            return Search(accounts, search);
        }

        public IEnumerable<InstagramAccount> Search(IEnumerable<InstagramAccount> accounts, string search)
        {
            search = search.ToLower();
            return accounts.Where(x => x.IgUserName.Contains(search) || x.Description.Contains(search));
        }

        public void Update(InstagramAccount account)
        {
            var accountDb = context.InstagramAccounts.FirstOrDefault(x => x.Id == account.Id);

            accountDb.IgUserName = account.IgUserName;
            accountDb.Description = account.Description;
            accountDb.TotalFollowers = account.TotalFollowers;
            accountDb.TotalPosts = account.TotalPosts;
            accountDb.AvgComments = account.AvgComments;
            accountDb.AvgLikes = account.AvgLikes;

            context.SaveChanges();
        }

        public void UpdateAccountCategory(InstagramAccount account, Category category)
        {
            var accountDb = context.InstagramAccounts.FirstOrDefault(x => x.Id == account.Id);

            accountDb.CategoryId = category.Id;
            accountDb.Category = category;

            context.SaveChanges();

        }
    }
}
