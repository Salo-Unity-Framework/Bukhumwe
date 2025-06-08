using UnityEngine;

/// <summary>
/// Part of the GameplayRun prefab, this maintains the list of enemies
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private RunRuntimeDataSO runtimeData;

    private void OnEnable()
    {
        RunEvents.OnEnemySpawned += handleEnemySpawned;
        RunEvents.OnEnemyHit += handleEnemyHit;
    }

    private void OnDisable()
    {
        RunEvents.OnEnemySpawned -= handleEnemySpawned;
        RunEvents.OnEnemyHit -= handleEnemyHit;
    }

    private void handleEnemySpawned(Enemy enemy)
    {
        if (!runtimeData.ActiveEnemies.ContainsKey(enemy.HitCollider))
        {
            runtimeData.ActiveEnemies.Add(enemy.HitCollider, enemy);
        }
    }

    private void handleEnemyHit(Enemy enemy)
    {
        // Enemies die in one hit
        runtimeData.ActiveEnemies.Remove(enemy.HitCollider);
    }
}
