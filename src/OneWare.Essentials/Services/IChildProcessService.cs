using OneWare.Essentials.Enums;

namespace OneWare.Essentials.Services;

public interface IChildProcessService
{
    public Task<(bool success, string output)> ExecuteShellAsync(string path, string arguments, string workingDirectory,
        string status, AppState state = AppState.Loading);
}