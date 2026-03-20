using Domain.Repositories;
using Domain.UnityOfWork;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Module
{
    public static class InfrastructureModuleExt
    {
        public static void AddInfrastructureModule(this IServiceCollection services)
        {
            services.AddScoped<IDeviceDataRepository, DeviceDataRepository>();
            services.AddDbContext<DatabaseContext> ();
            services.AddScoped<IUnityOfWork, UnityOfWork>();
        }

    }
}
