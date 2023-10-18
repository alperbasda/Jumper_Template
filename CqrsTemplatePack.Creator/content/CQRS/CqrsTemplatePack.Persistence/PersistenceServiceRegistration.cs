using Core.Persistence.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsTemplatePack.Persistence.Persistance;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services,IConfiguration configuration)
    {
        #region Db Options

        DatabaseOptions opts = new DatabaseOptions();

        configuration.GetSection("DatabaseOptions").Bind(opts);
        services.Configure<DatabaseOptions>(options =>
        {
            options = opts;
        });
        services.AddSingleton(sp =>
        {
            return opts;
        });


        #endregion



        return services;
    }
}
