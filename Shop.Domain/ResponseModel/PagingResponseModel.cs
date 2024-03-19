namespace Shop.Infratructure.Repository;

public class PagingResponseModel<T>
{
    public string? Message { get; set; }
    public int Total { get; set; }
    public T? Data { get; set; }
}