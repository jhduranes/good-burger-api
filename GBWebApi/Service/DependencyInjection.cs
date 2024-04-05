using Microsoft.Extensions.DependencyInjection;

namespace Service
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            #region Repository            
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            #endregion

            #region Service            
            services.AddScoped<IUsuarioService, UsuarioService>();
            #endregion
        }
    }
}
