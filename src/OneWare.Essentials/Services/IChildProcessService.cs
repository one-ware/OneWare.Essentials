using OneWare.Essentials.Enums;

namespace OneWare.Essentials.Services;

public interface IChildProcessService
{
    public Task<(bool success, string output)> ExecuteShellAsync(string path, IReadOnlyCollection<string> arguments, string workingDirectory,
        string status, AppState state = AppState.Loading, bool showTimer = false, Func<string, bool>? outputAction = null,
        Func<string, bool>? errorAction = null);
}