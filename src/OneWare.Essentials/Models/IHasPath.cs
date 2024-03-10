namespace OneWare.Essentials.Models;

public interface IHasPath
{
    public string FullPath { get; }
    public string Name { get; }
    public bool LoadingFailed { get; set; }
}