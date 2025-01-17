using UnityEngine;
using Zenject;

namespace MissTextChanger.Components;

internal class MissTextEffectSpawner : MonoBehaviour, IFlyingObjectEffectDidFinishEvent
{
    private float duration;
    private float xSpread;
    private float targetYPos;
    private float targetZPos;
    private Color color;
    private float fontSize;
    private MissTextEffect.Pool missTextEffectPool = null!;
    private PluginConfig pluginConfig = null!;

    [Inject]
    public void Init(
        [Inject(Id = "missEffectDuration")] float duration,
        [Inject(Id = "missEffectSpread")] float xSpread,
        [Inject(Id = "missEffectTargetYPos")] float targetYPos,
        [Inject(Id = "missEffectTargetZPos")] float targetZPos,
        [Inject(Id = "missEffectColor")] Color color,
        [Inject(Id = "missEffectFontSize")] float fontSize,
        MissTextEffect.Pool missTextEffectPool,
        PluginConfig pluginConfig)
    {
        this.duration = duration;
        this.xSpread = xSpread;
        this.targetYPos = targetYPos;
        this.targetZPos = targetZPos;
        this.color = color;
        this.fontSize = fontSize;
        this.missTextEffectPool = missTextEffectPool;
        this.pluginConfig = pluginConfig;
    }
    
    public void SpawnText(Vector3 pos, Quaternion rotation, Quaternion inverseRotation)
    {
        var missTextEffect = missTextEffectPool.Spawn();
        missTextEffect.didFinishEvent.Add(this);
        missTextEffect.transform.localPosition = pos;
        
        var targetPos = rotation * new Vector3(Mathf.Sign((inverseRotation * pos).x) * xSpread, targetYPos, targetZPos);
        var text = pluginConfig.MissText;
        
        missTextEffect.InitAndPresent(text, duration, targetPos, rotation, color, fontSize, false);
    }

    public void HandleFlyingObjectEffectDidFinish(FlyingObjectEffect flyingObjectEffect)
    {
        flyingObjectEffect.didFinishEvent.Remove(this);
        missTextEffectPool.Despawn((MissTextEffect)flyingObjectEffect);
    }
}