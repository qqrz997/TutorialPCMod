using MissTextChanger.Components;
using MissTextChanger.Patches;
using TMPro;
using UnityEngine;
using Zenject;

namespace MissTextChanger.Installers;

internal class PlayerInstaller : Installer
{
    private readonly PluginConfig pluginConfig;

    public PlayerInstaller(PluginConfig pluginConfig)
    {
        this.pluginConfig = pluginConfig;
    }

    public override void InstallBindings()
    {
        if (!pluginConfig.Enabled) return;
        
        Container.Bind<MissTextEffectSpawner>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesTo<OnMissEffectPatch>().AsSingle();
        Container.BindMemoryPool<MissTextEffect, MissTextEffect.Pool>()
            .WithInitialSize(20)
            .FromComponentInNewPrefab(GetMissTextEffectPrefab());
    }
    
    private static MissTextEffect GetMissTextEffectPrefab()
    {
        var prefabObject = new GameObject("MissTextEffect");
        var textEffect = prefabObject.AddComponent<MissTextEffect>();

        var textObject = new GameObject("Text") { layer = LayerMask.NameToLayer("UI") };
        textObject.transform.SetParent(prefabObject.transform, false);

        textEffect.textMesh = textObject.AddComponent<TextMeshPro>();
        textEffect.textMesh.alignment = TextAlignmentOptions.Capline;
        textEffect.textMesh.fontStyle = FontStyles.Bold | FontStyles.Italic;

        return textEffect;
    }
}
