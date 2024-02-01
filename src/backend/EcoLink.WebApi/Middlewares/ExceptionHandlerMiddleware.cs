using EcoLink.WebApi.Models;
using EcoLink.Application.Commons.Exceptions;

namespace EcoLink.WebApi.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
        catch (AlreadyExistException ex)
        {
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            logger.LogError(message: ex.ToString());
            await context.Response.WriteAsJsonAsync(new Response
            {
                Status = context.Response.StatusCode,
                Message = ex.Message,
            });
        }
    }
}