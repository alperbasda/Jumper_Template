﻿//---------------------------------------------------------------------------------------
//      This code was generated by a Jumper template tool.
//---------------------------------------------------------------------------------------
namespace CqrsTemplatePack.UI.Web.Helpers;

public class ScopeSafeServiceProvider
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public static IServiceCollection Create(IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();
        return services;
    }

}
