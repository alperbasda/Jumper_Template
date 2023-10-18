//#if {{usecache}}
//using Core.Application.Pipelines.Caching;
//#endif
//#if {{useserilog}}
//using Core.Application.Pipelines.Logging;
//#endif
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CqrsTemplatePack.Application.Base;

namespace CqrsTemplatePack.Application;

public static class ApplicationServiceRegistiration
{
    public static IServiceCollection AddApplicationServicez(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());

            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            configuration.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));

//#if {{usecache}}

//            configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
//            configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));

//#endif

//#if {{useserilog}}

//            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));

//#endif


            //Dİkkat !!! Aynı anda notification yakayan tablolarınızda aynı db tablosuna erişmeye çalışırsanız hata alırsınız.
            configuration.NotificationPublisher = new TaskWhenAllPublisher();
            configuration.NotificationPublisherType = typeof(TaskWhenAllPublisher);
            configuration.Lifetime = ServiceLifetime.Scoped;
        });

        return services;

    }



    public static IServiceCollection AddSubClassesOfType(
      this IServiceCollection services,
      Assembly assembly,
      Type type,
      Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null)
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }

}
