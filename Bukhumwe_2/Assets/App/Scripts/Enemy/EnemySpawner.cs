using UnityEngine;

/// <summary>
/// Part of the GameplayRun prefab, this spawns enemies
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    private bool isPlaying = false; // will spawn while playing only
    private float lastSpawnTime;

    // Time after lastSpawnTime to spawn again. This will decrease as the run progresses
    private float spawnIntervalSeconds;

    private void OnEnable()
    {
        RunEvents.OnRunStateChanged += handleRunStateChanged;
    }

    private void OnDisable()
    {
        RunEvents.OnRunStateChanged -= handleRunStateChanged;
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
        // TODO: Use a pool a create initial objects during scene loading

        var prefab = AppSOHolder.Instance.RunRuntimeData.LoadedEnemyPrefab;
        var spawnPosition = getRandomSpawnPosition();

        var enemy = Instantiate(prefab, spawnPosition, Quaternion.identity);

        enemy.Speed = generateSpeed();
        enemy.NormalizedDirection = generateNormalizedDirection(spawnPosition);

        RunEvents.EnemySpawned(enemy);
    }

    private float generateNextSpawnInterval()
    {
        // TODO: Random around a value
        // TODO: Decrease as time increases

        return 2f;
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

        return 1f;
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
