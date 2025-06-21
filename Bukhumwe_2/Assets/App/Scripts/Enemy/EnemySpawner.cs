using DG.Tweening;
using UnityEngine;

/// <summary>
/// Part of the GameplayRun prefab, this spawns enemies
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private RunConfigSO runConfig;
    [SerializeField] private RunRuntimeDataSO runRuntimeData;

    private bool isPlaying = false; // will spawn while playing only
    private float lastSpawnTime;

    // Time after lastSpawnTime to spawn again. This will decrease as the run progresses
    private float spawnIntervalSeconds;

    private void OnEnable()
    {
        RunEvents.OnRunStateChanged += handleRunStateChanged;
        RunEvents.OnEnemyReleaseReady += handleEnemyReleaseReady;
    }

    private void OnDisable()
    {
        RunEvents.OnRunStateChanged -= handleRunStateChanged;
        RunEvents.OnEnemyReleaseReady -= handleEnemyReleaseReady;
    }

    private void Update()
    {
        tryToSpawn();
    }

    private void handleRunStateChanged(RunState _, RunState newState)
    {
        isPlaying = (newState == RunState.Play);
    }

    private void tryToSpawn()
    {
        if (!isPlaying) return; // Spawn only during Play state
        if ((Time.time - lastSpawnTime) < spawnIntervalSeconds) return; // too soon after last spawn

        spawn();

        lastSpawnTime = Time.time;
        spawnIntervalSeconds = generateNextSpawnInterval();
    }

    private void spawn()
    {
        var spawnPosition = getRandomSpawnPosition();

        var enemy = EnemyPooler.Instance.Get();

        // Note: Enemy will Release itself to the pool when needed

        enemy.gameObject.transform.position = spawnPosition;
        enemy.Speed = generateSpeed();
        enemy.NormalizedDirection = generateNormalizedDirection(spawnPosition);

        RunEvents.EnemySpawned(enemy);
    }

    private void handleEnemyReleaseReady(Enemy enemy)
    {
        EnemyPooler.Instance.Release(enemy);
    }

    private float generateNextSpawnInterval()
    {
        var difficultyProfile = runConfig.DifficultyProfile;

        // The time on a scale of 0 to 1 in terms of spawn difficulty
        var timeFraction = (Time.time - runRuntimeData.StartTime) / difficultyProfile.SpawnIntervalMaxDiffSeconds;

        // Get the interval based on current time (timeFraction) and difficulty profile
        var interval = DOVirtual.EasedValue(
            difficultyProfile.SpawnIntervalAtStartSeconds,
            difficultyProfile.SpawnIntervalAtEndSeconds,
            timeFraction,
            difficultyProfile.SpawnIntervalCurve);

        // Random around a value. Eg: interval of 2 with sigma factor of 0.3
        var randomMultiplier = Random.Range(1 - difficultyProfile.SpawnIntervalSigma, 1 + difficultyProfile.SpawnIntervalSigma); // [0.7, 1.3]
        interval = interval * randomMultiplier; // [1.4, 2.6]

        return interval;
    }

    private Vector3 getRandomSpawnPosition()
    {
        var viewportExtent = AppSOHolder.Instance.RunRuntimeData.ViewportExtent;

        // Don't spawn too close to the edge horizontally
        var x = Random.Range(-0.8f * viewportExtent.x, 0.8f * viewportExtent.x);

        // Spawn out of the screen vertically
        var y = 1.2f * viewportExtent.y;

        return new Vector3(x, y);
    }

    private float generateSpeed()
    {
        // TODO: Random around a value
        // TODO: Increase as time increases

        return 3f;
    }

    private Vector3 generateNormalizedDirection(Vector3 spawnPosition)
    {
        var viewportExtent = AppSOHolder.Instance.RunRuntimeData.ViewportExtent;

        // Target random x and opposite y
        var x = Random.Range(-0.8f * viewportExtent.x, 0.8f * viewportExtent.x);
        var targetPosition = new Vector3(x, -spawnPosition.y);

        var direction = targetPosition - spawnPosition;

        return direction.normalized;
    }
}
