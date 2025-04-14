using System.Net;
using System.Reflection.Metadata.Ecma335;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Shared.ErrorModels;

namespace e_Commerce.MiddleWares
{
    public class GlopalErrorHandelingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlopalErrorHandelingMiddleWare> _logger;

        public GlopalErrorHandelingMiddleWare(RequestDelegate next , ILogger<GlopalErrorHandelingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandelNotFoundApiAsync(httpContext);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong {ex}");
                await HandelExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandelNotFoundApiAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The End Point {context.Request.Path} does not exist.",

            }.ToString();
            await context.Response.WriteAsync(response);


        }
        private async Task HandelExceptionAsync(HttpContext httpContext ,Exception ex)
        {

            //handeling sql error connection (database error )
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
               _ => (int)HttpStatusCode.InternalServerError
            };
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ErrorMessage = ex.Message
            }.ToString();
            await httpContext.Response.WriteAsync(response);
            //handeling sql error connection (database error )

        }
    }
}
