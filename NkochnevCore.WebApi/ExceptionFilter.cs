using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace NkochnevCore.WebApi
{
    public class ExceptionFilter : IExceptionFilter
    {
	    private readonly ILogger<ExceptionFilter> _logger;

	    public ExceptionFilter(ILogger<ExceptionFilter> logger)
	    {
		    _logger = logger;
	    }

	    public void OnException(ExceptionContext context)
	    {
		    var status = HttpStatusCode.InternalServerError;
		    var message = String.Empty;

		    var exceptionType = context.Exception.GetType();
		    if (exceptionType == typeof(UnauthorizedAccessException))
		    {
			    message = "Unauthorized Access";
			    status = HttpStatusCode.Unauthorized;
		    }
		    context.ExceptionHandled = true;

			_logger.LogError(new EventId(0),context.Exception, context.Exception.Message);

		    var response = context.HttpContext.Response;
		    response.StatusCode = (int)status;
		    response.ContentType = "application/json";
		    response.WriteAsync(message);
	    }
    }
}
