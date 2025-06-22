using UnityEngine;

/// <summary>
/// Part of the GameplayRun prefab, this listens to
/// run events and handles SFX and score popups
/// </summary>
public class RunFXManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private RunConfigSO runConfig;

    private void OnEnable()
    {
        RunEvents.OnEnemyHit += handleEnemyHit;
        RunEvents.OnHealthUpdated += handleHealthUpdated;
    }

    private void OnDisable()
    {
        RunEvents.OnEnemyHit -= handleEnemyHit;
        RunEvents.OnHealthUpdated -= handleHealthUpdated;
    }

    private void handleEnemyHit(Enemy enemy)
    {
        audioSource.PlayOneShot(runConfig.EnemyHitClip); // Play SFX

        // TODO: Score popup
    }

    private void handleHealthUpdated(int _, int __)
    {
        // This may also trigger on Start when setting health to max.
        // So process this during Play state only.
        if (AppSOHolder.Instance.RunRuntimeData.CurrentRunState != RunState.Play) return;

        audioSource.PlayOneShot(runConfig.PlayerHitClip); // Play SFX
    }
}
