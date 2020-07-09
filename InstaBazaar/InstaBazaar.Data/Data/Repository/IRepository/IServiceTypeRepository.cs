using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository.IRepository
{
    public interface IServiceTypeRepository : IRepository<ServiceType>
    {
        Task UpdateAsync(ServiceType serviceType);
    }
}
