namespace Shop.Domain.ResponseModel ;

public class PagingResponseModel<T> where T : class
{
    public int CurrentPage {  get; set; } 
    public int TotalPage { get; set; }
    public IEnumerable<T>? Data { get; set; }

    public PagingResponseModel( int total, IEnumerable<T>? data, int currentPage)
    {
        
        TotalPage = total;
        Data = data;
        CurrentPage = currentPage;
    }

    public PagingResponseModel()
    {
        
    }
}