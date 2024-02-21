using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Aplication.ResultOrError;

namespace Shop.Api.Extentions;

public static class Extention
{
   public static IActionResult ResultOrError<T>(this ControllerBase controller, object result)
   {
      Type objectType = result.GetType(); // Get Type object 
      
      if (objectType == typeof(Ok<T>)) // compare Type if true return  OkObjectResult
      {
         return controller.Ok(result );
      }

      if (objectType == typeof(NotFound<T>))
      {
         return controller.NotFound(result);
      }

      if (objectType == typeof(UnAuthorization<T>))
      {
         return controller.Unauthorized(result);
      }

      if (objectType == typeof(Created<T>))
      {
         return controller.Created(controller.HttpContext.Request.Path, result);
      }

      if (objectType == typeof(BadRequest<T>))
      {
         return controller.BadRequest(result);
      }

      if (objectType == typeof(ServerError<T>))
      {
         return controller.StatusCode(StatusCodes.Status500InternalServerError, result);
      }
      throw new ArgumentException("Type error");
   }
}