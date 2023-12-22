namespace OrgBloom.Application.Commons.Exceptions;

public class AlreadyExistException(string message) : Exception(message)
{
    public int StatusCode { get; set; } = 403;
}