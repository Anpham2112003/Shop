using Microsoft.AspNetCore.Mvc;

namespace Shop.Aplication.ResultOrError;

public interface  IResult<T>
{
    public  string? Message { get; set; }
    
}
public sealed class Ok<T>:IResult<T>
{
    public string? Message { get; set; }
   public  T? Data { get; set; }

   public Ok(string? message, T? data)
   {
       Message = message;
       Data = data;
   }
}

public sealed class NotFound<T>:IResult<T>
{
    public string? Message { get; set; }

    public NotFound(string? message)
    {
        Message = message;
    }
}
public sealed class Created<T>:IResult<T>
{
    public string? Message { get; set; }
    public T? Data { get; set; }

    public Created(string? message, T? data)
    {
        Message = message;
        Data = data;
    }
}

public sealed class BadRequest<T>:IResult<T>
{
    public string? Message { get; set; }

    public BadRequest(string? message)
    {
        Message = message;
    }
}
public sealed class UnAuthorization<T>:IResult<T>
{
    public string? Message { get; set; }

    public UnAuthorization(string? message)
    {
        Message = message;
    }
}

public sealed class ServerError<T>:IResult<T>
{
    public string? Message { get; set; }

    public ServerError(string? message)
    {
        Message = message;
    }
}

