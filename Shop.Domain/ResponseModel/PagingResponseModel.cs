namespace Shop.Domain.ResponseModel ;

public class PagingResponseModel<T>
{
    public string? Message { get; set; }
    public int Total { get; set; }
    public T? Data { get; set; }

    public PagingResponseModel(string? message, int total, T? data)
    {
        Message = message;
        Total = total;
        Data = data;
    }

    public PagingResponseModel()
    {
        
    }
}