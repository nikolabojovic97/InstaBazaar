using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using InstaBazaar.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private new ApplicationDbContext context;
        private readonly UserManager<User> userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null)
        {
            var users = await GetAllAsync(orderBy: orderBy, includeProperties: "Role");

            return users.ToList();
        }

        public async Task UpdateAsync(User user)
        {
            await Task.Run(() =>
            {

            });
        }

        public IEnumerable<User> Search(IEnumerable<User> users, string search)
        {
            search = search.ToLower();
            return users.Where(x => x.UserName.ToLower().Contains(search) || x.Email.ToLower().Contains(search)).ToList();
        }
    }
}
