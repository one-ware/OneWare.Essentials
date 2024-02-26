using OneWare.Essentials.NativeTools;

namespace OneWare.Essentials.Services;

public interface INativeToolService
{
    public NativeToolContainer Register(string key);
    public NativeToolContainer? Get(string key);
    public Task<bool> InstallAsync(NativeToolContainer container);
}