using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// Part of the TitleSceneManager prefab, this
/// loads resources for the title scene.
/// </summary>
public class TitleResourceLoader : SceneResourceLoaderBase
{
    [SerializeField] private TitleEnemyHandler titleEnemyHandler; // to assign loaded enemy

    private AsyncOperationHandle<Enemy> loadedHandle; // needed for unload

    public async override UniTask Load()
    {
        var enemyPrefabReference = AppSOHolder.Instance.RunConfig.EnemyPrefabReference;
        var handle = enemyPrefabReference.LoadAssetAsync();
        await handle.Task.AsUniTask();

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError("[TitleResourceLoader] Error loading Enemy prefab");
            return;
        }

        // Loaded successfully

        loadedHandle = handle;
        titleEnemyHandler.SetLoadedEnemyPrefab(handle.Result);
    }

    public override UniTask Unload()
    {
        AppSOHolder.Instance.RunConfig.EnemyPrefabReference.ReleaseInstance(loadedHandle);

        AppSOHolder.Instance.RunRuntimeData.LoadedEnemyPrefab = null;
        return UniTask.CompletedTask;
    }
}
