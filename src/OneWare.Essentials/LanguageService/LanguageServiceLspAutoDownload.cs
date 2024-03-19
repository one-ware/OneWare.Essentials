using OneWare.Essentials.Enums;
using OneWare.Essentials.PackageManager;
using OneWare.Essentials.Services;

namespace OneWare.Essentials.LanguageService;

public abstract class LanguageServiceLspAutoDownload : LanguageServiceLsp
{
    private readonly IPackageService _packageService;
    private readonly Package _package;
    private bool _enableAutoDownload = false;
    
    protected LanguageServiceLspAutoDownload(IObservable<string> executablePath, Package package, string name, string? workspace, IPackageService packageService, IObservable<bool> enableAutoDownload) 
        : base(name, workspace)
    {
        _package = package;
        _packageService = packageService;

        enableAutoDownload.Subscribe(x =>
        {
            _enableAutoDownload = x;
        });
        executablePath.Subscribe(x =>
        {
            ExecutablePath = x;
        });
    }
    
    public override async Task ActivateAsync()
    {
        if (_packageService.Packages.TryGetValue(_package.Id!, out var model) && model is {Status: PackageStatus.Available or PackageStatus.UpdateAvailable or PackageStatus.Installing})
        {
            if (!_enableAutoDownload) return;
            if(!await _packageService.InstallAsync(_package)) return;
        }
        await base.ActivateAsync();
    }
}