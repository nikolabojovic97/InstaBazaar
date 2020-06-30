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
        public IEnumerable<InstagramAccount> Search(string search)
        {
            search = search.ToLower();
            return context.InstagramAccounts.Where(x => x.IgUserName.Contains(search) || x.Description.Contains(search));
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
