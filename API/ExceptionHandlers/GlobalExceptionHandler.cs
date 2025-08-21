
using System.Net;
using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace API.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Unhandled exception occurred");

            var (statusCode, title, detail, errors) = exception switch
            {
                NotFoundException ex => ((int)HttpStatusCode.NotFound, "Not Found", ex.Message, (object?)null),
                ConflictException ex => ((int)HttpStatusCode.Conflict, "Conflict", ex.Message, (object?)null),
                UnauthorizedAccessException ex => ((int)HttpStatusCode.Unauthorized, "Unauthorized", ex.Message, (object?)null),

                _ => ((int)HttpStatusCode.InternalServerError, 
                      "Internal Server Error", 
                      GetSafeMessage(exception), 
                      (object?)null)
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail, // ✅ Clean detail message
                Instance = httpContext.Request.Path,
                Type = $"https://httpstatuses.com/{statusCode}"
            };

            // If validation errors exist, include them in extensions
            if (errors != null)
            {
                problemDetails.Extensions["errors"] = errors;
            }

            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/problem+json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static string GetSafeMessage(Exception exception)
        {
            // Strip "Value cannot be null. (Parameter '...')" -> return only inner message
            if (exception is ArgumentNullException argEx && !string.IsNullOrEmpty(argEx.ParamName))
            {
                return argEx.ParamName; 
            }

            // For others just return the message
            return exception.Message;
        }
    }