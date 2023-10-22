using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.CrossCuttingConcerns.Serilog;
using Core.Persistence.Models.Responses;
using Serilog;

namespace CqrsTemplatePack.UI.Api.Middlewares;
public static class ExceptionHandlerMiddlewareExtension
{
    #region For Use

    public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlerMiddleware>();
    }

    #endregion
}
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    //Exception logger kullanmak isterseniz yorum satırlarını kaldırabilirsiniz.
    //LoggerServiceBase _loggerServiceBase;
    public ExceptionHandlerMiddleware(RequestDelegate next /*, LoggerServiceBase loggerServiceBase*/)
    {
        _next = next;
        //_loggerServiceBase = loggerServiceBase;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException error)
        {

            var errors = string.Join("<br/>", error.Errors.SelectMany(w => w.Errors));
            await context.Response.WriteAsJsonAsync(Response<MessageResponse>.Fail(errors, 400));
        }
        catch (BusinessException error)
        {
            await context.Response.WriteAsJsonAsync(Response<MessageResponse>.Fail(error.Message, 400));
        }
        catch (NotFoundException error)
        {
            await context.Response.WriteAsJsonAsync(Response<MessageResponse>.Fail(error.Message, 400));
        }
        catch (AuthorizationException error)
        {
            await context.Response.WriteAsJsonAsync(Response<MessageResponse>.Fail(error.Message, 400));
        }
        catch (Exception error)
        {
            await context.Response.WriteAsJsonAsync(Response<MessageResponse>.Fail("İşlem sırasında bir hata oluştu.", 500));
        }

    }

}