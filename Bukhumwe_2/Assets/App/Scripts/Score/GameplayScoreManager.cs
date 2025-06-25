using Salo.Infrastructure;
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
        RunEvents.OnPlayerDeath += handlePlayerDeath;
    }

    private void OnDisable()
    {
        RunEvents.OnEnemyHit -= handleEnemyHit;
        RunEvents.OnPlayerDeath -= handlePlayerDeath;
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

    private void handlePlayerDeath()
    {
        // Save scores on game over
        var lastScore = AppSOHolder.Instance.RunRuntimeData.Score;
        var playerStatsRuntimeData = AppSOHolder.Instance.PlayerStatsRuntimeData;

        playerStatsRuntimeData.LastScore = lastScore;

        if (lastScore > playerStatsRuntimeData.HighScore)
        {
            playerStatsRuntimeData.HighScore = lastScore;
        }

        // Persist to disk
        playerStatsRuntimeData.Save();
    }
}
