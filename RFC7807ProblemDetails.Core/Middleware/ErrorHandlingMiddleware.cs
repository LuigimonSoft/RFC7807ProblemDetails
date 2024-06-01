using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RFC7807ProblemDetails.Core.Exceptions;
using RFC7807ProblemDetails.Core.Models;
using System.Globalization;
using System.Net;
using System.Resources;
using System.Text.Json;

namespace RFC7807ProblemDetails.Core.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware>? _logger;
        private readonly List<Error> _errorlist;
        private readonly ResourceManager? _resourceManager;
        private string language = "en-US";
        private readonly bool _detailsInProduction = false;

        public ErrorHandlingMiddleware(RequestDelegate next, List<Error> errorlist, ResourceManager? resourceManager = null, bool detailsInProduction = false, ILogger<ErrorHandlingMiddleware>? logger = null)
        {
            _next = next;
            _logger = logger;
            _errorlist = errorlist;
            _resourceManager = resourceManager;
            _detailsInProduction = detailsInProduction;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (_logger != null)
                    _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (context.Request.Headers.ContainsKey("Accept-Language"))
                language = context.Request.Headers["Accept-Language"];
            string type = context.Request.Host.Value + context.Request.Path.Value + context.Request.QueryString.Value;
            string instance = context.Request.Path.Value + context.Request.QueryString.Value;


            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string Title = "An error occurred while processing your request.";
            string Detail = exception.Message;
            int errorCode = 0;

            if (exception is BaseException)
            {
                var exceptionBase = (BaseException)exception;
                errorCode = exceptionBase.ErrorCode;

            }

            if (_errorlist.Any(x => x.ErrorCode == errorCode))
            {
                var error = _errorlist.First(x => x.ErrorCode == errorCode);
                httpStatusCode = error.StatusCode;
                if (_resourceManager != null)
                    Title = _resourceManager.GetString(error.ErrorCode.ToString(), new CultureInfo(language)) ?? error.ErrorMessage;
                else
                    Title = error.ErrorMessage ?? string.Empty;
            }

            if (_logger != null)
            {
                if (exception is BaseException)
                {
                    string typeException = exception.GetType().Name;
                    if (exception is RepositoryException)
                        typeException = "RepositoryException";
                    else if (exception is ServiceException)
                        typeException = "ServiceException";
                    else if (exception is ControllerException)
                        typeException = "ControllerException";
                    _logger.LogError(exception.InnerException, $"Error type: {typeException}, Error Code: {errorCode}, Error message: {exception.Message}");
                }
                else
                    _logger.LogError(exception, exception.Message);
            }

            var problemDetail = new ProblemDetail
            {
                Type = type,
                Title = exception.Message,
                Status = (int)httpStatusCode,
                ErrorCode = errorCode.ToString(),

                Instance = instance,
                AdditionalProperties = new List<AdditionalInfo>()
            };

#if (DEBUG || _detailsInProduction)
            problemDetail.Detail = exception.InnerException.Message;
#endif

            problemDetail.AdditionalProperties.Add(new AdditionalInfo("method", context.Request.Method));

            var result = JsonSerializer.Serialize(problemDetail);

            context.Response.StatusCode = (int)httpStatusCode;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsync(result);
        }
    }
}
