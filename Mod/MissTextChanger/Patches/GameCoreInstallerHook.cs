using SiraUtil.Affinity;
using UnityEngine;

namespace MissTextChanger.Patches;

internal class GameCoreInstallerHook : IAffinity
{
    [AffinityPrefix]
    [AffinityPatch(typeof(GameplayCoreInstaller), nameof(GameplayCoreInstaller.InstallBindings))]
    private void InstallBindingsPostfix(GameplayCoreInstaller __instance)
    {
        var container = __instance.Container;
        var missSpriteSpawner = __instance._missedNoteEffectSpawnerPrefab._missedNoteFlyingSpriteSpawner;
        var flyingTextEffect = __instance._effectPoolsManualInstaller._flyingTextEffectPrefab;
        
        float duration = missSpriteSpawner._duration;
        float spread = missSpriteSpawner._xSpread;
        float targetYPos = missSpriteSpawner._targetYPos;
        float targetZPos = missSpriteSpawner._targetZPos;
        var color = Color.white;
        const float fontSize = 4.5f; // Miss text is a sprite; estimate the font size
        var fadeAnimationCurve = flyingTextEffect._fadeAnimationCurve;
        var moveAnimationCurve = flyingTextEffect._moveAnimationCurve;

        container.BindInstance(duration).WithId("missEffectDuration").AsCached();
        container.BindInstance(spread).WithId("missEffectSpread").AsCached();
        container.BindInstance(targetYPos).WithId("missEffectTargetYPos").AsCached();
        container.BindInstance(targetZPos).WithId("missEffectTargetZPos").AsCached();
        container.BindInstance(color).WithId("missEffectColor").AsCached();
        container.BindInstance(fontSize).WithId("missEffectFontSize").AsCached();
        container.BindInstance(fadeAnimationCurve).WithId("textEffectFadeAnimationCurve").AsCached();
        container.BindInstance(moveAnimationCurve).WithId("textEffectMoveAnimationCurve").AsCached();
    }
}