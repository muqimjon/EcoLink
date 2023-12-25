namespace EcoLink.Application.Commons.Exceptions;

public class NotFoundException(string message) : Exception(message)
{
    public int StatusCode { get; set; } = 404;
}
