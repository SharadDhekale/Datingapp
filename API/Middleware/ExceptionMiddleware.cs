using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _looger;
        private readonly IHostEnvironment _hostEnvironment;
        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> looger,
        IHostEnvironment hostEnvironment)
        {
            _looger = looger;
            _requestDelegate = requestDelegate;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _looger.LogError(ex, ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode= (int) HttpStatusCode.InternalServerError;
                var response= _hostEnvironment.IsDevelopment()
                                ? new ApiException( context.Response.StatusCode,ex.Message,ex.StackTrace )
                                : new ApiException(context.Response.StatusCode,"Internal Server Error");
                  var option= new JsonSerializerOptions{ PropertyNamingPolicy = JsonNamingPolicy.CamelCase};      
                  var josonresponse= JsonSerializer.Serialize(response,option);
                  await context.Response.WriteAsync(josonresponse);    

            }
        }
    }
}