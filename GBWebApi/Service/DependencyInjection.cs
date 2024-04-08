using Domain.Interfaces.Repository;
using Domain.Interfaces.Service;
using Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace Service
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            #region Repository            
            services.AddScoped<IMenuRepository, MenuRepository>();
            #endregion

            #region Service            
            services.AddScoped<IMenuService, MenuService>();
            #endregion
        }
    }
}
