
using FluentValidation;

namespace Shop.Api.Middleware
{
    public class HandErrorMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
	        try
	        {
		        await next(context);
	        }
	        catch (ValidationException validationException)
	        {
		        context.Response.StatusCode = StatusCodes.Status400BadRequest;
		        context.Response.ContentType = "application/json";
		        await context.Response.WriteAsJsonAsync(new
		        {
			        validationException.Errors
		        });
	        }
			catch (Exception ex)
			{
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				context.Response.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(new
				{
					status = StatusCodes.Status500InternalServerError,
					message = "Internal Server Error : "+ex.Message
				});
			}
        }
    }
}
