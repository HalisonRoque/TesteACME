using System.Text.Json;
using AcmeClinic.Application.Exceptions;

namespace AcmeClinic.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException error)
        {
            context.Response.StatusCode = error.StatusCode;

            context.Response.ContentType = "application/json";

            await context.Response
            .WriteAsync(
                JsonSerializer
                .Serialize(
                    new
                    {
                        error = error.Message
                    }
                )
            );
        }
        catch (
            Exception
        )
        {
            context.Response.StatusCode = 500;

            await context.Response
            .WriteAsync(
                JsonSerializer
                .Serialize(
                    new
                    {
                        error = "Erro interno"
                    }
                )
            );
        }
    }
}