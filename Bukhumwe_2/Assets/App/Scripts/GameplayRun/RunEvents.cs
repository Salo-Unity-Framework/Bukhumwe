using System;

public static class RunEvents
{
    /// <summary>
    /// An enemy was hit
    /// </summary>
    public static event Action<Enemy> OnEnemyHit;
    public static void EnemyHit(Enemy enemy)
        => OnEnemyHit?.Invoke(enemy);
}
