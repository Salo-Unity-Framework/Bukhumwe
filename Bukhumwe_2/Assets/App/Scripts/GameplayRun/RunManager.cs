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

        // TODO: Listen for game over
    }

    private void OnDisable()
    {
        SceneLoadEvents.OnSceneReady -= handleSceneReady;

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

        setRunState(RunState.Play);
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
