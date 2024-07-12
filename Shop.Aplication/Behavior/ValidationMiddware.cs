using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Aplication.Behavior
{
    public class ValidationMiddware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next(context);
			}
			catch (ValidationException e)
			{

				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.Response.Headers.ContentType = "application/json";
				await context.Response.WriteAsJsonAsync(e.Errors);
			}
			catch (Exception e) 
			{
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.Headers.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(e);
            }
        }
    }
}
