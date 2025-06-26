using DG.Tweening;
using UnityEngine;

/// <summary>
/// Part of the PlayerBase prefab, handles interactions
/// with the Player base object
/// </summary>
public class PlayerBase : MonoBehaviour
{
    // NOTE: The camera's vertical viewport is preserved across
    // different aspect ratios. Placing the base correctly
    // at the bottom during edit time should work.

    [SerializeField] private RunConfigSO runConfig;
    [SerializeField] private RunRuntimeDataSO runRuntimeData;

    [SerializeField] private Transform visuals;

    private readonly Vector3 HIT_SHAKE_STRENGTH = new Vector3(0.1f, 0.02f, 0);
    private const float HIT_SHAKE_SECONDS = 0.1f;

    private const float ANIMATION_SECONDS = 1f;

    private void OnEnable()
    {
        RunEvents.OnRunStateChanged += handleRunStateChanged;
    }

    private void OnDisable()
    {
        RunEvents.OnRunStateChanged -= handleRunStateChanged;
    }

    private void Start()
    {
        // Set starting health
        runRuntimeData.CurrentPlayerHealth = runConfig.StartintPlayerHealth;
        RunEvents.HealthUpdated(runConfig.StartintPlayerHealth, runRuntimeData.CurrentPlayerHealth);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore if the collided object was not an Enemy
        if (collision.gameObject.layer != AppSOHolder.Instance.RunConfig.EnemyLayer) return;

        // An enemy has hit the player. Enemy will handle its own trigger event

        // Decrease health and trigger event
        runRuntimeData.CurrentPlayerHealth -= runConfig.EnemyDamage;
        RunEvents.HealthUpdated(runConfig.EnemyDamage, runRuntimeData.CurrentPlayerHealth);

        visuals.DOShakePosition(HIT_SHAKE_SECONDS, HIT_SHAKE_STRENGTH, 200, 180);
    }

    private void handleRunStateChanged(RunState _, RunState newState)
    {
        switch (newState)
        {
            case RunState.Intro:

                // Ascend into view on Intro
                visuals.DOLocalMoveY(0, ANIMATION_SECONDS)
                    .From(-1f).SetEase(Ease.OutCubic);

                break;

            case RunState.Outro:

                // Descend out of view on game over after the delay
                visuals.DOLocalMoveY(-1f, ANIMATION_SECONDS)
                    .SetEase(Ease.Linear)
                    .SetDelay(runConfig.GameOverPauseMilliseconds / 1000f);

                break;
        }
    }
}
