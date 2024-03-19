using OneWare.Essentials.Enums;
using OneWare.Essentials.Models;
using OneWare.Essentials.Packages;
using OneWare.Essentials.Services;
using Prism.Ioc;

namespace OneWare.Essentials.LanguageService;

public abstract class LanguageServiceLspAutoDownload : LanguageServiceLsp
{
    private readonly IPackageService _packageService;
    private Package _package;
    
    protected LanguageServiceLspAutoDownload(IObservable<string> executablePath, Package package, string name, string? workspace, IPackageService packageService) 
        : base(name, workspace)
    {
        _package = package;
        _packageService = packageService;
        
        executablePath.Subscribe(x =>
        {
            ExecutablePath = x;
        });
    }
    
    public override async Task ActivateAsync()
    {
        if (_packageService.Packages.TryGetValue(_package.Id!, out var model) && model is {Status: PackageStatus.Available or PackageStatus.UpdateAvailable})
        {
            if(!await _packageService.InstallAsync(_package)) return;
        }
        await base.ActivateAsync();
    }
}