using MissTextChanger.Components;
using SiraUtil.Affinity;

namespace MissTextChanger.Patches;

internal class OnMissEffectPatch : IAffinity
{
    private readonly MissTextEffectSpawner missTextEffectSpawner;

    public OnMissEffectPatch(MissTextEffectSpawner missTextEffectSpawner)
    {
        this.missTextEffectSpawner = missTextEffectSpawner;
    }

    [AffinityPrefix]
    [AffinityPatch(typeof(MissedNoteEffectSpawner), nameof(MissedNoteEffectSpawner.HandleNoteWasMissed))]
    private bool HandleNoteWasMissedPrefix(MissedNoteEffectSpawner __instance, NoteController noteController)
    {
        if (noteController.hidden
            || noteController.noteData.time + 0.5f < __instance._audioTimeSyncController.songTime
            || noteController.noteData.colorType == ColorType.None)
        {
            // Do nothing
            return false;
        }

        var position = noteController.inverseWorldRotation * noteController.noteTransform.position;
        position.z = __instance._spawnPosZ;

        // Spawn our miss text effect 
        missTextEffectSpawner.SpawnText(
            noteController.worldRotation * position,
            noteController.worldRotation,
            noteController.inverseWorldRotation);
        
        // Cancel the original implementation
        return false;
    }
}