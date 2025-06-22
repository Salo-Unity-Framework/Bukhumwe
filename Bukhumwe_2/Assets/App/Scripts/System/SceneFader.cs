using Cysharp.Threading.Tasks;
using DG.Tweening;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

// This bootstrapped system (on SceneFader prefab)
// handles scene fade throughout the game
public class SceneFader : SceneFaderBase
{
    [SerializeField] private Image faderImage;

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneLoadEvents.OnSceneReady += handleSceneReady;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        SceneLoadEvents.OnSceneReady -= handleSceneReady;
    }

    public async override UniTask FadeOut()
    {
        // NOTE: Scripting define symbol UNITASK_DOTWEEN_SUPPORT
        // is required for tweens to be UniTask awaitable.
        await faderImage.DOFade(1f, AppSOHolder.Instance.AppFlowConfig.SceneFadeOutSeconds)
            .SetEase(Ease.Linear);
    }

    // Implement a fade in to occur when the scene is ready
    private void handleSceneReady()
    {
        faderImage.DOFade(0f, AppSOHolder.Instance.AppFlowConfig.SceneFadeInSeconds)
            .SetEase(Ease.Linear);
    }
}
