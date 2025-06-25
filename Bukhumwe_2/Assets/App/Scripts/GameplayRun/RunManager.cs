using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Part of the GameplayRun prefab, this controls the run flow
/// </summary>
public class RunManager : MonoBehaviour
{
    [SerializeField] private RunConfigSO runConfig;
    [SerializeField] private RunRuntimeDataSO runRuntimeData;

    private void OnEnable()
    {
        SceneLoadEvents.OnSceneReady += handleSceneReady;
        RunEvents.OnHealthUpdated += handleHealthUpdated;
    }

    private void OnDisable()
    {
        SceneLoadEvents.OnSceneReady -= handleSceneReady;
        RunEvents.OnHealthUpdated -= handleHealthUpdated;

        if (Application.isPlaying) setRunState(RunState.None);
    }

    private void Start()
    {
        // Set viewport extents
        var mainCamera = Camera.main;
        Assert.IsNotNull(mainCamera);

        var y = mainCamera.orthographicSize;
        var x = (y * Screen.width) / Screen.height;
        runRuntimeData.ViewportExtent = new Vector2(x, y);
    }

    private async void handleSceneReady()
    {
        setRunState(RunState.Intro);

        // Wait before starting gameplay
        await UniTask.Delay(runConfig.RunIntroMilliseconds);

        runRuntimeData.StartTime = Time.time;
        setRunState(RunState.Play);
    }

    private void handleHealthUpdated(int _, int updatedHealth)
    {
        // Trigger player death on 0 health
        if (updatedHealth <= 0)
        {
            processPlayerDeath().Forget();
        }
    }

    private async UniTaskVoid processPlayerDeath()
    {
        setRunState(RunState.Outro);
        RunEvents.PlayerDeath();

        await PlayerDeathHandler.Instance.HandlePlayerDeathAsync();

        // Load title scene after a small delay
        await UniTask.Delay(500);
        SceneLoadEvents.TitleSceneLoadRequested();
    }

    private void setRunState(RunState newState)
    {
        // Ignore if already in the state
        if (newState == runRuntimeData.CurrentRunState) return;

        var oldState = runRuntimeData.CurrentRunState;

        runRuntimeData.CurrentRunState = newState;
        RunEvents.RunStateChanged(oldState, newState);
    }
}
