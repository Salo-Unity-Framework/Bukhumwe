using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// This loader is part of the GameplayRun prefab. It loads assets for the run.
/// This is to showcase how scene loaders can be used to load heavier assets.
/// </summary>
public class GameplayResourceLoader : SceneResourceLoaderBase
{
    private AsyncOperationHandle<Enemy> loadedHandle; // needed for unload

    public override async UniTask Load()
    {
        var runRuntimeData = AppSOHolder.Instance.RunRuntimeData;

        // Do not reload if already loaded. This should not happen
        if (null != runRuntimeData.LoadedEnemyPrefab) return;

        var enemyPrefabReference = AppSOHolder.Instance.RunConfig.EnemyPrefabReference;
        var handle = enemyPrefabReference.LoadAssetAsync();
        await handle.Task.AsUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("[GameplayResourceLoader] Error loading Enemy prefab");
            return;
        }

        // Loaded successfully. Save the handle and the loaded prefab.
        // The loaded prefab can now be Instantiated when needed.

        loadedHandle = handle;
        runRuntimeData.LoadedEnemyPrefab = handle.Result;

        // Initialize and warmup the enemy pool
        await EnemyPooler.Instance.InitializeAsync();
    }

    public override UniTask Unload()
    {
        //AppSOHolder.Instance.RunConfig.EnemyPrefabReference.ReleaseAsset(); // does not work
        AppSOHolder.Instance.RunConfig.EnemyPrefabReference.ReleaseInstance(loadedHandle); // works

        AppSOHolder.Instance.RunRuntimeData.LoadedEnemyPrefab = null;
        return UniTask.CompletedTask;
    }

    // Debug force release memory
    [NaughtyAttributes.Button]
    private void debugGCCollect()
    {
        System.GC.Collect();
        Resources.UnloadUnusedAssets();
    }
}
