namespace Myteka.Infrastructure.Exceptions;

/// <summary>
/// Occurs in the absence of the desired result
/// </summary>
public class GuidNotValidException : Exception
{
    public string Message { get; set; }
    public GuidNotValidException(string message)
    {
        Message = message;
    }
}