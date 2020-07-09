using InstaBazaar.Data.Data.Repository.IRepository;
using InstaBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InstaBazaar.Data.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private new readonly ApplicationDbContext context;

        public ServiceRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task UpdateAsync(Service service)
        {
            await Task.Run( async () =>
            {
                var serviceDb = await context.Services.FindAsync(service.InstagramAccountId, service.ServiceTypeId);

                serviceDb.Price = service.Price;
            }); 
        }
    }
}
