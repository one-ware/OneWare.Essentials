using OneWare.Essentials.Enums;
using OneWare.Essentials.Models;

namespace OneWare.Essentials.Services;

public interface IApplicationStateService
{
    public ApplicationProcess ActiveProcess { get; }
    
    public ApplicationProcess AddState(string status, AppState state, Action? terminate = null);

    public void RemoveState(ApplicationProcess key, string finishMessage = "Done");
    
    public Task TerminateActiveDialogAsync();
}