using BLL.Interfaces;
using BLL.Interfaces.Identity;
using BLL.Managers.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Providers
{
    public static class BLLServices
    {
        public static IServiceCollection AddBLLServices(this IServiceCollection services)
        {
            services.AddScoped<IDataManager, DataManager>();
            services.AddScoped<Users>();

            return services;
        }
    }
}
