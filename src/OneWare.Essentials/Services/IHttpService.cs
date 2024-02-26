﻿using Avalonia.Media.Imaging;

namespace OneWare.Essentials.Services;

public interface IHttpService
{
    public HttpClient HttpClient { get; }

    public Task<bool> DownloadFileAsync(string url, Stream stream, IProgress<float>? progress = null,
        TimeSpan timeout = default, CancellationToken cancellationToken = default);
    
    public Task<bool> DownloadFileAsync(string url, string location, IProgress<float>? progress = null, TimeSpan timeout = default,
        CancellationToken cancellationToken = default);

    public Task<bool> DownloadAndExtractArchiveAsync(string url, string location, IProgress<float>? progress = null,
        TimeSpan timeout = default, CancellationToken cancellationToken = default);
    
    public Task<Bitmap?> DownloadImageAsync(string url, TimeSpan timeout = default,
        CancellationToken cancellationToken = default);
    
    public Task<string?> DownloadTextAsync(string url, TimeSpan timeout = default, 
        CancellationToken cancellationToken = default);
}