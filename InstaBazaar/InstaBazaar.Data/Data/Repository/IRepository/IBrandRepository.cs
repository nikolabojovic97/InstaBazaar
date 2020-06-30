using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IBrandRepository : IRepository<Brand>
    {
        IEnumerable<Brand> Search(string search);
        void Update(Brand brand);
    }
}
