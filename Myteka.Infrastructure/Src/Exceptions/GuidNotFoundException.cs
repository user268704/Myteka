namespace Myteka.Infrastructure.Exceptions;

/// <summary>
/// Occurs in the absence of the desired result
/// </summary>
public class GuidNotFoundException : Exception
{
    public string Message { get; set; }
    public GuidNotFoundException(string message)
    {
        Message = message;
    }
}