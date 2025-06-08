using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Part of the GameplayRun prefab, this handles enemy pooling.
/// It is a part of the scene loading process.
/// </summary>
public class EnemyPooler : StaticInstanceOf<EnemyPooler>
{
    private ObjectPool<Enemy> pool;

    // During warmup, object instantiation will be moved to the next frame
    // if the last instantiations took too long. This ensures framerate
    // remains smooth without prolonging loading.
    private float currentBatchStartTime;
    private const float BATCH_SECONDS = 0.005f; // 5ms

    private const int DEFAULT_CAPACITY = 10;

    public Enemy Get() => pool.Get();
    public void Release(Enemy enemy) => pool.Release(enemy);

    // This should be called after loading in the Enemy prefab
    public async UniTask InitializeAsync()
    {
        var prefab = AppSOHolder.Instance.RunRuntimeData.LoadedEnemyPrefab;

        pool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(prefab),
            actionOnGet: getEnemy,
            actionOnRelease: releaseEnemy,
            actionOnDestroy: e => Destroy(e),
            collectionCheck: Application.isEditor, // Safety check during development
            defaultCapacity: DEFAULT_CAPACITY, // 10
            maxSize: 25
        );

        await warmUpAsync();
    }

    private void getEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        // TODO: Enable colliders if needed
    }

    private void releaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private async UniTask warmUpAsync()
    {
        var enemies = new Enemy[DEFAULT_CAPACITY];
        currentBatchStartTime = Time.realtimeSinceStartup;

        for (int i = 0; i < DEFAULT_CAPACITY; i++)
        {
            enemies[i] = pool.Get();

            // Continue instantiating in the next frame if the time threshold was crossed
            if (Time.realtimeSinceStartup - currentBatchStartTime >= BATCH_SECONDS)
            {
                await UniTask.Yield();
                currentBatchStartTime = Time.realtimeSinceStartup;
            }
        }

        for (int i = 0; i < DEFAULT_CAPACITY; i++)
        {
            pool.Release(enemies[i]);
        }
    }
}
