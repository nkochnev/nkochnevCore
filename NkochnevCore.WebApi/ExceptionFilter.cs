using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NkochnevCore.WebApi
{
    public class ExceptionFilter : IExceptionFilter
    {
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

		    var response = context.HttpContext.Response;
		    response.StatusCode = (int)status;
		    response.ContentType = "application/json";
		    response.WriteAsync(message);
	    }
    }
}
