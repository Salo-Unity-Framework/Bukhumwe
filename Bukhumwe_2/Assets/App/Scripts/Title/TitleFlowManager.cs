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
        RunEvents.OnEnemyHit += handleEnemyHit;
    }

    private void OnDisable()
    {
        RunEvents.OnEnemyHit -= handleEnemyHit;
    }

    private async void handleEnemyHit(Enemy _)
    {
        // The title scene has a single enemy that can be hit to start gameplay

        var appFlowConfig = AppSOHolder.Instance.AppFlowConfig;

        // Load the gameplayScene after a delay to wait for effects
        await UniTask.Delay(appFlowConfig.GameplayLoadDelayMilliseconds);

        SceneLoadEvents.MajorSceneLoadRequested(appFlowConfig.GameplayScene);
    }
}
