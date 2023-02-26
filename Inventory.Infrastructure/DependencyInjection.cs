using Inventory.Core.Interfaces;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection serviceDescriptors, IConfiguration conf)
        {

          serviceDescriptors.AddScoped<IITemRepository, ItemRepository>();
          serviceDescriptors.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            serviceDescriptors.AddDbContext<ApplicationDbContext>(x =>
            {
                x.UseSqlServer(conf.GetConnectionString("DefaultConnection"));
            });
            serviceDescriptors.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ApplicationDbContext>();


            return serviceDescriptors;
        }
    }
}
