using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Part of the GameplayRun prefab, this handles ScorePopup object
/// pooling. It is a part of the scene loading process.
/// </summary>
public class ScorePopupPooler : StaticInstanceOf<ScorePopupPooler>
{
    private ObjectPool<ScorePopup> pool;

    // During warmup, object instantiation will be moved to the next frame
    // if the last instantiations took too long. This ensures framerate
    // remains smooth without prolonging loading.
    private float currentBatchStartTime;
    private const float BATCH_SECONDS = 0.005f; // 5ms

    private const int DEFAULT_CAPACITY = 10;

    public ScorePopup Get() => pool.Get();
    public void Release(ScorePopup scorePopup) => pool.Release(scorePopup);

    // This should be called after loading in the prefab
    public async UniTask InitializeAsync()
    {
        var prefab = AppSOHolder.Instance.RunRuntimeData.LoadedScorePopupPrefab;

        pool = new ObjectPool<ScorePopup>(
            createFunc: () => Instantiate(prefab),
            actionOnGet: get,
            actionOnRelease: release,
            actionOnDestroy: e => Destroy(e),
            collectionCheck: Application.isEditor, // Safety check during development
            defaultCapacity: DEFAULT_CAPACITY, // 10
            maxSize: 25
        );

        await warmUpAsync();
    }

    private void get(ScorePopup scorePopup)
    {
        scorePopup.gameObject.SetActive(true);
    }

    private void release(ScorePopup scorePopup)
    {
        scorePopup.gameObject.SetActive(false);
    }

    private async UniTask warmUpAsync()
    {
        var items = new ScorePopup[DEFAULT_CAPACITY];
        currentBatchStartTime = Time.realtimeSinceStartup;

        for (int i = 0; i < DEFAULT_CAPACITY; i++)
        {
            items[i] = pool.Get();

            // Continue instantiating in the next frame if the time threshold was crossed
            if (Time.realtimeSinceStartup - currentBatchStartTime >= BATCH_SECONDS)
            {
                await UniTask.Yield();
                currentBatchStartTime = Time.realtimeSinceStartup;
            }
        }

        for (int i = 0; i < DEFAULT_CAPACITY; i++)
        {
            pool.Release(items[i]);
        }
    }
}
