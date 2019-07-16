using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;
using VehicleTracking.Data.CommonData;
using VehicleTracking.Data.Exceptions;

namespace VehicleTracking.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string response;
            switch (exception)
            {
                //Custom error showing to user
                case CustomException e:
                    response = JsonConvert.SerializeObject(e);
                    Log.Error("Error Code: {@ErrorCode} with Message: {@ErrorMessage}", e.ErrorCode, e.ErrorMessage);
                    break;
                //Exception by system
                default:
                    //return system Error and log error to DB
                    var sb = new StringBuilder();
                    var currentException = exception;

                    while (currentException != null)
                    {
                        sb.AppendLine(currentException.StackTrace);
                        currentException = currentException.InnerException;
                    }

                    response = JsonConvert.SerializeObject(new CustomException(ErrorCode.E500));

                    Log.Error("Error: {@Message} with StackTrace: {@sb}", exception.Message, sb.ToString());
                    break;
            }
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(response);
        }
    }
}
