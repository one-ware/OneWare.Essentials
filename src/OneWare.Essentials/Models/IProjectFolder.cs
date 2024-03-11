namespace OneWare.Essentials.Models;

public interface IProjectFolder : IProjectEntry
{
    public void Add(IProjectEntry entry);
    
    public void Remove(IProjectEntry entry);

    public IProjectFile AddFile(string path, bool createNew = false);

    public IProjectFolder AddFolder(string path, bool createNew = false);

    public IProjectEntry? SearchName(string path, bool recursive = true);
    
    public IProjectEntry? SearchRelativePath(string path, bool recursive = true);
    
    public IProjectEntry? SearchFullPath(string path, bool recursive = true);
}