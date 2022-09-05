using Myteka.Models.InternalModels;

namespace Myteka.FileRepository;

public class FileMetaRecover
{
    public ContentMetadata Recover(byte[] file, Content meta)
    {
        if (meta.Metadata == null) 
            meta.Metadata = new();

        SizeSet(file.Length, meta);
        TypeSet(meta);
        UpdateDateSet(meta);
        
        return meta.Metadata;
    }

    private void UpdateDateSet(Content meta)
    {
        meta.Metadata.UpdateDate = DateTime.Now;
    }

    private void SizeSet(int fileLength, Content meta)
    {
        int sizeInKb = fileLength / 1024;
        meta.Metadata.Size = sizeInKb;
    }
    
    private void TypeSet(Content meta)
    {
        string fileExtension = Path.GetExtension(meta.FileName[1..]);
        meta.Metadata.Type = fileExtension;
    }
}