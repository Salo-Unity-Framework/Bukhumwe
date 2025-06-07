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

        runRuntimeData.LoadedEnemyPrefab = handle.Result;
    }

    public override UniTask Unload()
    {
        AppSOHolder.Instance.RunConfig.EnemyPrefabReference.ReleaseAsset();
        AppSOHolder.Instance.RunRuntimeData.LoadedEnemyPrefab = null;
        return UniTask.CompletedTask;
    }
}
