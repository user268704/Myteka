namespace Myteka.FileRepository;

public class Allocator
{
    public static string GetFilePath(Guid fileId)
    {
        string idString = fileId.ToString();
        
        string folderName = idString.Substring(0, 3);
        string folderPath = @"D:\Myteka\Files\" + folderName;

        return folderPath;
    }
    
    public static void CreateFolder(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}