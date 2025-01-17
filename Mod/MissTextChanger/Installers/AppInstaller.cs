using MissTextChanger.Patches;
using Zenject;

namespace MissTextChanger.Installers;

internal class AppInstaller(PluginConfig pluginConfig) : Installer
{
    private readonly PluginConfig pluginConfig = pluginConfig;
    
    public override void InstallBindings()
    {
        Container.BindInstance(pluginConfig).AsSingle();
        Container.BindInterfacesTo<GameCoreInstallerHook>().AsSingle();
    }
}