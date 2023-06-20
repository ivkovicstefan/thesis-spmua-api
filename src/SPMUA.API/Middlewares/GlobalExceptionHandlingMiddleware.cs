using Microsoft.AspNetCore.Mvc;
using SPMUA.Model.Commons.ErrorHandling;
using SPMUA.Model.Dictionaries.Commons;
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

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMesssage.ValidationErrorTitle,
					Description = ErrorDetailsMesssage.ValidationErrorDescription,
					Errors = ex.Errors
				};

                await context.Response.WriteAsJsonAsync(errorDetails);
            }
			catch (EntityNotFoundException ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMesssage.EntityNotFoundErrorTitle,
					Description = String.Format(ErrorDetailsMesssage.EntityNotFoundErrorDescription, ex.EntityId)
				};

                await context.Response.WriteAsJsonAsync(errorDetails);
			}
            catch (Exception)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMesssage.InternalServerErrorTitle,
					Description = ErrorDetailsMesssage.InternalServerErrorDescription
				};

                await context.Response.WriteAsJsonAsync(errorDetails);
			}
        }
    }
}
