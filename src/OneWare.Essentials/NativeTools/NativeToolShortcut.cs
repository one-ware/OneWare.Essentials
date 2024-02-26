using CommunityToolkit.Mvvm.ComponentModel;

namespace OneWare.Essentials.NativeTools;

public class NativeToolShortcut(string relativePath, string? settingKey = null) : ObservableObject
{
    public string RelativePath { get; } = relativePath;
    public string? SettingKey { get; } = settingKey;
}