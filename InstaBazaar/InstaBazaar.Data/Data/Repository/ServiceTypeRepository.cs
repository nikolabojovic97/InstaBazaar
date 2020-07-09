using InstaBazaar.Data.Data.Repository;
using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data
{
    public class ServiceTypeRepository : Repository<ServiceType>, IServiceTypeRepository
    {
        private new readonly ApplicationDbContext context;

        public ServiceTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task UpdateAsync(ServiceType serviceType)
        {
            await Task.Run( async () =>
            {
                var serviceTypeDb = await context.ServiceTypes.FirstOrDefaultAsync(x => x.Id == serviceType.Id);

                serviceTypeDb.Name = serviceType.Name;
                serviceTypeDb.OrderNumber = serviceType.OrderNumber;
            });
        }
    }
}
