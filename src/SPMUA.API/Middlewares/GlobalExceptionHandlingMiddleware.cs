using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SPMUA.API.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				ProblemDetails details = new()
				{
					Type = "Server error",
					Title = "Internal Server Error",
					Detail = "An error occurred while processing the request."
				};

				await context.Response.WriteAsJsonAsync<ProblemDetails>(details);
			}
        }
    }
}
