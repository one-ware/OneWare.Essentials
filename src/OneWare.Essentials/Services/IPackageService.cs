﻿using OneWare.Essentials.Models;
using OneWare.Essentials.Packages;

namespace OneWare.Essentials.Services;

public interface IPackageService
{
    public Dictionary<string, PackageModel> Packages { get; }
    
    public event EventHandler? UpdateStarted;
    
    public event EventHandler? UpdateEnded;

    public void RegisterPackage(Package package);
    
    public void RegisterPackageRepository(string url);

    public Task<bool> LoadPackagesAsync();

    public Task<bool> InstallAsync(Package package);
}