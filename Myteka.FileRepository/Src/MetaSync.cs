using Myteka.Models.InternalModels;

namespace Myteka.FileRepository;

public class MetaSync
{
    // Синхронизация метаданных из файлового хранилища в базу данных
    public byte[] Sync(ContentMetadata metadata, byte[] file)
    {
        return file;
    }
    
    // Синхронизация метаданных PDF файла
    private void SyncPdfMeta(ContentMetadata metadata, byte[] file)
    {
        
    }

    // Определитель типа файла по расширению
    private string GetFileType(string fileName, string mimeType = "")
    {
        throw new NotImplementedException();
    }
}