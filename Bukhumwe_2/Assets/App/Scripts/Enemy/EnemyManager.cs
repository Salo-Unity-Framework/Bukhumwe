using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEngine;

/// <summary>
/// Part of the GameplayRun prefab, this maintains the list of enemies
/// </summary>
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private RunRuntimeDataSO runRuntimeData;

    private void OnEnable()
    {
        RunEvents.OnEnemySpawned += handleEnemySpawned;
        RunEvents.OnEnemyReleaseReady += handleEnemyReleaseReady;
        RunEvents.OnPlayerDeath += handlePlayerDeath;
    }

    private void OnDisable()
    {
        RunEvents.OnEnemySpawned -= handleEnemySpawned;
        RunEvents.OnEnemyReleaseReady -= handleEnemyReleaseReady;
        RunEvents.OnPlayerDeath -= handlePlayerDeath;
    }

    private void handleEnemySpawned(Enemy enemy)
    {
        // Add to active enemy list, making sure it's not already there
        if (!runRuntimeData.ActiveEnemies.ContainsKey(enemy.HitCollider))
        {
            runRuntimeData.ActiveEnemies.Add(enemy.HitCollider, enemy);
        }
    }

    private void handleEnemyReleaseReady(Enemy enemy)
    {
        // Remove a released enemy from the list. Note that this occurs some
        // time after the enemy is hit. Colliders should be disabled when
        // the enemy is hit since it is not removed from the list then.
        runRuntimeData.ActiveEnemies.Remove(enemy.HitCollider);
    }

    private async void handlePlayerDeath()
    {
        // Release enemies on player death when the death animations start.
        // Using ToList() since ActiveEnemies will be modified in the loop.
        await UniTask.Delay(AppSOHolder.Instance.RunConfig.GameOverPauseMilliseconds);
        foreach (var enemy in runRuntimeData.ActiveEnemies.Values.ToList())
        {
            // Invoke the event instead of directly releasing so other systems can handle the event
            RunEvents.EnemyReleaseReady(enemy);
        }
    }
}
