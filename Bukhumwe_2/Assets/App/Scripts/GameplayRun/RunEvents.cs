using System;

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
}
