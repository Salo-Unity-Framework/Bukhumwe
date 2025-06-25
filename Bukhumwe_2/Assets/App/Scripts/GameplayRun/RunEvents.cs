using System;
using UnityEngine;

public static class RunEvents
{
    /// <summary>
    /// Gameplay run state has changed. Parameters are <oldState, newState>.
    /// Invoked by RunManager.
    /// </summary>
    public static event Action<RunState, RunState> OnRunStateChanged;
    public static void RunStateChanged(RunState oldState, RunState newState)
        => OnRunStateChanged?.Invoke(oldState, newState);

    /// <summary>
    /// An enemy has spawned. Invoked by EnemySpawner
    /// </summary>
    public static event Action<Enemy> OnEnemySpawned;
    public static void EnemySpawned(Enemy enemy)
        => OnEnemySpawned?.Invoke(enemy);

    /// <summary>
    /// An enemy was hit
    /// </summary>
    public static event Action<Enemy> OnEnemyHit;
    public static void EnemyHit(Enemy enemy)
        => OnEnemyHit?.Invoke(enemy);

    /// <summary>
    /// The enemy is now ready to be released back to the pool
    /// </summary>
    public static event Action<Enemy> OnEnemyReleaseReady;
    public static void EnemyReleaseReady(Enemy enemy)
        => OnEnemyReleaseReady?.Invoke(enemy);

    /// <summary>
    /// Score was updated on the RunRuntimeData SO during a run
    /// </summary>
    public static event Action<ScoreEventArgs> OnScoreUdpated;
    public static void ScoreUpdated(ScoreEventArgs eventArgs)
        => OnScoreUdpated?.Invoke(eventArgs);

    /// <summary>
    /// Player health has changed. This could be from a reset or player hit.
    /// Parameters are <healthDelta, updatedHealth>
    /// </summary>
    public static event Action<int, int> OnHealthUpdated;
    public static void HealthUpdated(int healthDelta, int updatedHealth)
        => OnHealthUpdated?.Invoke(healthDelta, updatedHealth);

    /// <summary>
    /// The Player has died. This is similar to checking for Outro state
    /// </summary>
    public static event Action OnPlayerDeath;
    public static void PlayerDeath()
        => OnPlayerDeath?.Invoke();

    public struct ScoreEventArgs
    {
        public int scoreDelta;
        public int updatedScore;
        public Vector3 scorePosition;
    }
}
