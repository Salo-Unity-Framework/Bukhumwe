using UnityEngine;

/// <summary>
/// Part of the GameplayRun prefab, this listens to gameplay
/// events and updates the score on the runtime data SO.
/// </summary>
public class GameplayScoreManager : MonoBehaviour
{
    private void OnEnable()
    {
        RunEvents.OnEnemyHit += handleEnemyHit;
    }

    private void OnDisable()
    {
        RunEvents.OnEnemyHit -= handleEnemyHit;
    }

    private void Start()
    {
        // Reset score
        AppSOHolder.Instance.RunRuntimeData.Score = 0;
        RunEvents.ScoreUpdated(new RunEvents.ScoreEventArgs()
        {
            scoreDelta = 0,
            updatedScore = AppSOHolder.Instance.RunRuntimeData.Score,
            scorePosition = Vector3.zero,
        });
    }

    private void handleEnemyHit(Enemy enemy)
    {
        // Calculate the score from the enemy, add it, and invoke the event
        var score = (int)(enemy.Speed * AppSOHolder.Instance.RunConfig.EnemySpeedToScore);
        AppSOHolder.Instance.RunRuntimeData.Score += score;

        RunEvents.ScoreUpdated(new RunEvents.ScoreEventArgs()
        {
            scoreDelta = score,
            updatedScore = AppSOHolder.Instance.RunRuntimeData.Score,
            scorePosition = enemy.transform.position,
        });
    }
}
