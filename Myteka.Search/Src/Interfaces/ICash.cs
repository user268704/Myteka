namespace Myteka.Search.Interfaces;

public interface ICash
{
    public void SetCash(string cash);
    public string GetCash(Guid cashId);
    public void DeleteCash(Guid cashId);
    public bool ContainsCash(Guid cashId);
}