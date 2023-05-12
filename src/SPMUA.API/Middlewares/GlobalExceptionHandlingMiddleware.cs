using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.Exceptions;
using System.ComponentModel.DataAnnotations;
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
			catch (RequestValidationException ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

				var details = new
				{
					Title = "Validation Error",
					Errors = ex.Errors,
					Message = ex.Message
				};

                await context.Response.WriteAsJsonAsync(details);
            }
			catch (EntityNotFoundException)
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;

				ProblemDetails details = new()
				{
					Type = "Not found error",
					Title = "Resource Not Found",
					Detail = "Resource that you are looking for is not found."
				};

				await context.Response.WriteAsJsonAsync(details);
			}
            catch (Exception)
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
