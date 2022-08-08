namespace Myteka.Infrastructure.Exceptions;

public class FileAlreadyExistsException : Exception
{
    public string FileName { get; set; }
    public FileAlreadyExistsException(string fileName)
    {
        FileName = fileName;
    }

    public FileAlreadyExistsException()
    { }
}