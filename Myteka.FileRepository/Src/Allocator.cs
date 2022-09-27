using Myteka.Configuration;

namespace Myteka.FileRepository;

public class Allocator
{
    public static string CreateFilePath(Guid fileId)
    {
        string idString = fileId.ToString();
        
        string folderName = idString.Substring(0, 3);

        var conf = Config.GetConfig();
        string folderPath = conf.Get().Global.FileSavePath + folderName;

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