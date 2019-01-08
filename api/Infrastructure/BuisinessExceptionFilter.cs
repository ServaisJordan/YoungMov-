using System;
using System.Net;
using DTO;
using model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Exceptions;

namespace api.Infrastructure
{
    public class BusinessExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public BusinessExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("api.Exceptions");
        }

        void IExceptionFilter.OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unknow error occured");
            Exception exception = context.Exception;
            string contentType = "application/json";

            if (context.Exception.GetType()==typeof(DbUpdateConcurrencyException))
            {
               context.Result = new ContentResult() {
                   StatusCode = (int) HttpStatusCode.Conflict,
                   ContentType = contentType
               };
                
            } else
            if (context.Exception.GetType().IsSubclassOf(typeof(BusinessException)))
            {
                var result = new ContentResult()
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = Newtonsoft.Json.JsonConvert.SerializeObject(new DTO.Error() { Message = context.Exception.Message }),
                    ContentType = contentType

                };
                context.Result = result;
            } 
            else if (exception.GetType() == typeof (System.Data.SqlClient.SqlException)) {
                context.Result = new ContentResult() {
                    StatusCode = (int) HttpStatusCode.InternalServerError,
                    Content = "An unexpected error occurred",
                    ContentType = contentType
                };
            }
        }
    }
}
