using TMPro;
using UnityEngine;
using Zenject;

namespace MissTextChanger.Components;

internal class MissTextEffect : FlyingObjectEffect
{
    public class Pool : MonoMemoryPool<MissTextEffect>;
    
    private AnimationCurve fadeAnimationCurve = null!;

    [Inject]
    public void Init(
        [Inject(Id = "textEffectFadeAnimationCurve")] AnimationCurve fadeAnimationCurve,
        [Inject(Id = "textEffectMoveAnimationCurve")] AnimationCurve moveAnimationCurve)
    {
        this.fadeAnimationCurve = fadeAnimationCurve;
        _moveAnimationCurve = moveAnimationCurve;
    }
    
    [SerializeField] public TextMeshPro? textMesh;
    
    private Color color;

    public void InitAndPresent(string text, float duration, Vector3 targetPos, Quaternion rotation, Color color, float fontSize, bool shake)
    {
        if (textMesh == null) return;
        
        this.color = color;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        InitAndPresent(duration, targetPos, rotation, shake);
    }

    public override void ManualUpdate(float t)
    {
        if (textMesh != null)
        {
            textMesh.color = color with { a = fadeAnimationCurve.Evaluate(t) };
        }
    }
}