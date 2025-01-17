using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using IPA.Logging;
using MissTextChanger.Installers;
using SiraUtil.Zenject;

namespace MissTextChanger;

[Plugin(RuntimeOptions.SingleStartInit), NoEnableDisable]
internal class Plugin
{
    [Init]
    public Plugin(Logger log, Config config, PluginMetadata metadata, Zenjector zenjector)
    {
        log.Info($"{metadata.Name} {metadata.HVersion} initialized.");

        var pluginConfig = config.Generated<PluginConfig>();
        
        zenjector.UseLogger(log);
        zenjector.Install<AppInstaller>(Location.App, pluginConfig); 
        zenjector.Install<MenuInstaller>(Location.Menu);
        zenjector.Install<PlayerInstaller>(Location.Player); 
    }
}