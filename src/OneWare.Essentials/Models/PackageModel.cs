using CommunityToolkit.Mvvm.ComponentModel;
using OneWare.Essentials.Enums;
using OneWare.Essentials.Helpers;
using OneWare.Essentials.Packages;
using OneWare.Essentials.Services;

namespace OneWare.Essentials.Models;

public abstract class PackageModel(
    Package package,
    string packageType,
    string extractionFolder,
    IHttpService httpService,
    ILogger logger,
    IApplicationStateService applicationStateService)
    : ObservableObject
{
    private PackageVersion? _installedVersion;
    public PackageVersion? InstalledVersion
    {
        get => _installedVersion;
        set => SetProperty(ref _installedVersion, value);
    }
    
    private string? _warningText;
    public string? WarningText
    {
        get => _warningText;
        set => SetProperty(ref _warningText, value);
    }
    
    private PackageStatus _status;
    public PackageStatus Status
    {
        get => _status;
        set => SetProperty(ref _status, value);
    }
    
    private float _progress;
    public float Progress
    {
        get => _progress;
        private set => SetProperty(ref _progress, value);
    }
    
    protected string ExtractionFolder { get; } = extractionFolder;

    protected string PackageType { get; } = packageType;

    public event EventHandler? Installed;

    public event EventHandler? Removed;

    public Package Package { get; set; } = package;

    public async Task<bool> UpdateAsync(PackageVersion version)
    {
        if(!await RemoveAsync()) return false;
        return await DownloadAsync(version);
    }
    
    public async Task<bool> DownloadAsync(PackageVersion version)
    {
        try
        {
            Status = PackageStatus.Installing;
            
            var currentTarget = PlatformHelper.Platform.ToString().ToLower();

            var selectedVersion = version;
            
            var target = Package.Versions?
                .FirstOrDefault(x => x == selectedVersion)?
                .Targets?.FirstOrDefault(x => x.Target?.Replace("-", "") == currentTarget);

            if (target is {Url: not null})
            {
                var state = applicationStateService.AddState($"Downloading {Package.Id}...", AppState.Loading);
                
                var progress = new Progress<float>(x =>
                {
                    Progress = x;
                    state.StatusMessage = $"Downloading {Package.Id} {(int)(x*100)}%";
                });
                
                //Download
                if (!await httpService.DownloadAndExtractArchiveAsync(target.Url, ExtractionFolder, progress))
                {
                    Status = PackageStatus.Available;
                    return false;
                }
                
                applicationStateService.RemoveState(state);
                
                Install(target);
                
                InstalledVersion = selectedVersion;
                
                Installed?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                throw new NotSupportedException("Target not found!");
            }
        }
        catch (Exception e)
        {
            logger.Error(e.Message, e);
            Status = PackageStatus.Available;
            return false;
        }
        return true;
    }

    /// <summary>
    /// Gets called after downloading and extracting
    /// Make sure to set Status after completing
    /// </summary>
    protected abstract void Install(PackageTarget target);

    /// <summary>
    /// Can be used to stop processes to avoid removal issues
    /// </summary>
    protected virtual Task PrepareRemoveAsync(PackageTarget target)
    {
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Gets called after deleting the package
    /// Make sure to set Status after completing
    /// </summary>
    protected abstract void Uninstall();

    public async Task<bool> RemoveAsync()
    {
        if (Package.Id == null) throw new NullReferenceException(nameof(Package.Id));

        var currentTarget = PlatformHelper.Platform.ToString().ToLower();
        
        var target = InstalledVersion!.Targets?.FirstOrDefault(x => x.Target?.Replace("-", "") == currentTarget);
        
        if(target != null) await PrepareRemoveAsync(target);
        
        if (Directory.Exists(ExtractionFolder))
        {
            try
            {
                Directory.Delete(ExtractionFolder, true);
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                return false;
            }
        }
        
        InstalledVersion = null;
        
        Uninstall();
        
        Removed?.Invoke(this, EventArgs.Empty);

        return true;
    }
}