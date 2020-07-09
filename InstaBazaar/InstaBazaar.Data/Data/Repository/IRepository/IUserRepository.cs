using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersAsync(Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null);
        Task UpdateAsync(User user);
        IEnumerable<User> Search(IEnumerable<User> users, string search);
    }
}
