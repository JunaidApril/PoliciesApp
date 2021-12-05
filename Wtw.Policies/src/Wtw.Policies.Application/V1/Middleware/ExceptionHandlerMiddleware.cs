using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Wtw.Policies.Domain.Dtos;
using Wtw.Policies.Domain.Enums;

namespace Wtw.Policies.Application.BFF.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger,
            IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            var controller = context.Request.RouteValues["controller"]?.ToString();
            var action = context.Request.RouteValues["action"]?.ToString();

            var businessError = new BusinessErrorDto
            {
                Message = exception.Message,
                BusinessErrorCode = BusinessErrorCode.ServerError,
                HttpStatusCode = HttpStatusCode.BadRequest,
            };

            _logger.LogError(exception, "Server error has occurred on {Controller}/{Action}", controller, action);

            await context.Response.WriteAsync(JsonConvert.SerializeObject(businessError));
        }
    }
}
