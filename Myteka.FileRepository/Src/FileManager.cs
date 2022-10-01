using Myteka.Configuration;
using Myteka.Configuration.Models;
using Myteka.Models.InternalModels;

namespace Myteka.FileRepository;

public class FileManager
{
    ConfigModel Configuration;
    private static List<string> AllowedExtensions;

    public FileManager()
    {
        Configuration = Config.GetConfig().Get();
        
        SetAllowedExtensions();
    }
    
    /// <summary>
    /// Main method to save the file
    /// </summary>
    /// <param name="contentData"></param>
    /// <param name="file"></param>
    public void Save(Content contentData, byte[] file)
    {
        string filePath = Allocator.CreateFilePath(contentData.Id);
        string fileFullPath = filePath + "/" + contentData.FileName;
        Allocator.CreateFolder(filePath);

        contentData.Path = fileFullPath;
        MetaSync metaSync = new MetaSync();
        byte[] fileWithMetadata = metaSync.Sync(contentData.Metadata, file);
        
        File.WriteAllBytes(fileFullPath, fileWithMetadata);
    }

    public void Remove(Content contentData)
    {
        string directoryPath = Allocator.CreateFilePath(contentData.Id);
        string fileFullPath = directoryPath + "/" + contentData.FileName;

        if (File.Exists(fileFullPath))
        {
            File.Delete(fileFullPath);
            Directory.Delete(directoryPath);
        }
    }

    private void SetAllowedExtensions()
    {
        string allowedExtensions = File.ReadAllText(Configuration.Global.AllowedExtensions);
        
        AllowedExtensions = allowedExtensions.Split("\r\n").ToList();
    }
    
    public string[] GetSupportedFileExtension()
    {
        return AllowedExtensions.ToArray();
    }
    
    public bool IsExtensionAllowed(string fileName) =>
        !AllowedExtensions.Any(fileName.EndsWith);
}