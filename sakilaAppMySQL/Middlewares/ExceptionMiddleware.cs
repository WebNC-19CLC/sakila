using System.Net;
using System.Security;
using sakilaAppMySQL.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace sakilaAppMySQL.Middlewares
{
  public class ExceptionMiddleware
  {
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
    {
      _logger = logger;
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "An error has been occurred");
        await HandleExceptionAsync(context, ex);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
      string message = exception.Message;
      object? info = null;

      switch (exception)
      {
        case DbUpdateConcurrencyException:
          context.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
          message = "ConcurrentUpdate";
          break;

        case NotFoundException:
          context.Response.StatusCode = (int)HttpStatusCode.NotFound;
          break;

        case BusinessException:
          context.Response.StatusCode = (int)HttpStatusCode.NotAcceptable;
          break;

        case SecurityException:
          context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
          break;

        case ArgumentException:
          context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
          break;

        case HttpRequestException:
          context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
          break;

        default:
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
          message = "ServerError";
          break;
      }

      if (exception is BusinessException businessException)
      {
        info = businessException.Info;
      }

      var response = context.Response;
      response.ContentType = "application/json";
      return response.WriteAsync(JsonConvert.SerializeObject(new { Message = message, Info = info }));
    }
  }
}
