using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Media;

namespace OneWare.Essentials.Models;

/// <summary>
/// Can be a file or a folder
/// </summary>
public interface IProjectEntry : IProjectExplorerNode, IHasPath
{
    public ReadOnlyObservableCollection<IProjectEntry> Entities { get; }
    
    public string RelativePath { get; }

    public IProjectRoot Root { get; }
    
    public IProjectFolder? TopFolder { get; set; }

    public Action<Action<string>>? RequestRename { get; set; }
    
    public bool IsValid();
}