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
					Title = ErrorDetailsMessage.ValidationErrorTitle,
					Description = ErrorDetailsMessage.ValidationErrorDescription,
					Errors = ex.Errors
				};

                await context.Response.WriteAsJsonAsync(errorDetails);
            }
			catch (UnauthorizedRequestException ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMessage.UnauthorizedRequestErrorTitle,
					Description = ex.Message ?? ErrorDetailsMessage.UnauthorizedRequestErrorDescription
				};

				await context.Response.WriteAsJsonAsync(errorDetails);
			}
			catch (InvalidCredentialsException)
			{
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMessage.InvalidCredentialsErrorTitle,
					Description = ErrorDetailsMessage.InvalidCredentialsErrorDesciption
				};

				await context.Response.WriteAsJsonAsync(errorDetails);
			}
			catch (EntityNotFoundException ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMessage.EntityNotFoundErrorTitle,
					Description = String.Format(ErrorDetailsMessage.EntityNotFoundErrorDescription, ex.EntityId)
				};

                await context.Response.WriteAsJsonAsync(errorDetails);
			}
            catch (Exception ex)
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				ErrorDetails errorDetails = new()
				{
					Title = ErrorDetailsMessage.InternalServerErrorTitle,
					Description = ErrorDetailsMessage.InternalServerErrorDescription
				};

                await context.Response.WriteAsJsonAsync(errorDetails);
			}
        }
    }
}
