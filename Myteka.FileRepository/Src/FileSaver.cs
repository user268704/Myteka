using Myteka.Models.InternalModels;

namespace Myteka.FileRepository;

public class FileSaver
{
    public void Save(Content contentData, byte[] file)
    {
        string filePath = Allocator.GetFilePath(contentData.Id);
        string fileFullPath = filePath + @"\" + contentData.FileName;
        Allocator.CreateFolder(filePath);

        contentData.Path = fileFullPath;
        MetaSync metaSync = new MetaSync();
        byte[] fileWithMetadata = metaSync.Sync(contentData.Metadata, file);
        
        File.WriteAllBytes(fileFullPath, fileWithMetadata);
    }
}