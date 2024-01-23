namespace EcoLink.ApiService.Models.Commons;

public class Response<T>
{
    public int Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T Data { get; set; } = default!;
}
