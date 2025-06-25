using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;

/// <summary>
/// Part of the TitleSceneManager prefab, this dictates the app flow.
/// Basically this loads the gameplay scene.
/// </summary>
public class TitleFlowManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneLoadEvents.OnSceneReady += handleSceneReady;
        RunEvents.OnEnemyHit += handleEnemyHit;
    }

    private void OnDisable()
    {
        SceneLoadEvents.OnSceneReady -= handleSceneReady;
        RunEvents.OnEnemyHit -= handleEnemyHit;
    }

    private void handleSceneReady()
    {
        BukhumweController.Instance.ShowTitle();
    }

    private async void handleEnemyHit(Enemy _)
    {
        // The title scene has a single enemy that can be hit to start gameplay

        var appFlowConfig = AppSOHolder.Instance.AppFlowConfig;

        // Animate the title
        await BukhumweController.Instance.PlayGameStartAsync();

        // Load the gameplayScene a little after the start effects completes
        await UniTask.Delay(250);
        SceneLoadEvents.MajorSceneLoadRequested(appFlowConfig.GameplayScene);
    }
}
