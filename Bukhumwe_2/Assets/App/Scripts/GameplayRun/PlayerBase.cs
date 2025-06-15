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
    }
}
