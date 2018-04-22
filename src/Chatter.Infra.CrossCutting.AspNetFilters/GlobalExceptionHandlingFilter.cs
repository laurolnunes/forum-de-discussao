using System;
using Chatter.Application.Interfaces;
using Chatter.Application.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;

namespace Chatter.Infra.CrossCutting.AspNetFilters
{
    public class GlobalExceptionHandlingFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionHandlingFilter> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogApplicationService _logService;

        public GlobalExceptionHandlingFilter(ILogger<GlobalExceptionHandlingFilter> logger, IHostingEnvironment hostingEnvironment, ILogApplicationService logService)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
            _logService = logService;
        }

        public void OnException(ExceptionContext context)
        {
            if (_hostingEnvironment.IsProduction())
            {
                _logger.LogError(1, context.Exception, context.Exception.Message);
                var data = new
                {
                    Version = "v1.0",
                    User = context.HttpContext.User.Identity.Name,
                    IP = context.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Hostname = context.HttpContext.Request.Host.ToString(),
                    AreaAccessed = context.HttpContext.Request.GetDisplayUrl(),
                    Action = context.ActionDescriptor.DisplayName,
                    TimeStamp = DateTime.Now
                };

                _logService.Add(new LogViewModel
                {
                    Created = DateTime.Now,
                    Name = "Exception",
                    Message = context.Exception.Message,
                    Type = 1,
                    Details = data.ToString()
                });
            }

            var result = new ViewResult
            {
                ViewName = "Error"
            };
            var modelData = new EmptyModelMetadataProvider();

            result.ViewData = new ViewDataDictionary(modelData, context.ModelState)
            {
                {"ErrorMEssage", context.Exception.Message }
            };
            context.ExceptionHandled = true;
            context.Result = result;
        }
    }
}