using Core.ApiHelpers.JwtHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsTemplatePack.UI.Api.Middlewares;

public static class AddApiRegistrationServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TokenParameters>();
        return services;
    }

}
