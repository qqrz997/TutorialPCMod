using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using TMPro;
using Zenject;

namespace MissTextChanger.Menu;

[HotReload(RelativePathToLayout = @".\settingsView.bsml")]
[ViewDefinition("MissTextChanger.Menu.settingsView.bsml")]
internal class SettingsViewController : BSMLAutomaticViewController
{
    [Inject] private readonly PluginConfig pluginConfig = null!;

    [UIComponent("MissText")] private readonly TextMeshProUGUI missText = null!;

    [UIAction("#post-parse")]
    private void PostParse()
    {
        SetMissTextPreview(pluginConfig.MissText);
    }

    private bool Enabled
    {
        get => pluginConfig.Enabled;
        set => pluginConfig.Enabled = value;
    }

    private string KeyboardInput
    {
        get => pluginConfig.MissText;
        set
        {
            pluginConfig.MissText = value;
            SetMissTextPreview(value);
        }
    }
    
    private void SetMissTextPreview(string v) => 
        missText.text = string.IsNullOrEmpty(v) ? "<alpha=#AA>No miss text" : v;
}