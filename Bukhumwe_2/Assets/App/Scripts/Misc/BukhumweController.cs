using Cysharp.Threading.Tasks;
using DG.Tweening;
using Salo.Infrastructure;
using TMPro;
using UnityEngine;

/// <summary>
/// This script on the Bukhumwe prefab listens to events
/// to control the text visibility an animations
/// </summary>
public class BukhumweController : StaticInstanceOf<BukhumweController>
{
    [SerializeField] private TMP_Text bukhumweText; // Complete text

    // Animated texts will grow and fade over static ones
    [SerializeField] private GameObject buStatic;
    [SerializeField] private TMP_Text buAnimated;
    [SerializeField] private GameObject khuStatic;
    [SerializeField] private TMP_Text khuAnimated;
    [SerializeField] private GameObject mweStatic;
    [SerializeField] private TMP_Text mweAnimated1;
    [SerializeField] private TMP_Text mweAnimated2;
    [SerializeField] private TMP_Text mweAnimated3;

    // Called by TitleFlowManager. Animate the title in
    public void ShowTitle()
    {
        bukhumweText.gameObject.SetActive(true);
    }

    // Called by TitleFlowManager. Animate the title out
    public async UniTask PlayGameStartAsync()
    {
        _ = bukhumweText.DOFade(0, 0.3f);

        await bukhumweText.transform.DOScale(2f, 1f)
            .SetEase(Ease.OutCubic);

        bukhumweText.gameObject.SetActive(false);
    }

    // Called by PlayerDeathHandler
    public async UniTask PlayGameOverAsync()
    {
        // BU
        buStatic.SetActive(true);
        buAnimated.gameObject.SetActive(true);
        _ = buAnimated.DOFade(0, 0.6f).From(1f);
        _ = buAnimated.transform.DOScale(1.6f, 0.6f).From(1f)
            .SetEase(Ease.OutCubic);

        await UniTask.Delay(170);

        // KHU
        khuStatic.SetActive(true);
        khuAnimated.gameObject.SetActive(true);
        _ = khuAnimated.DOFade(0, 0.6f).From(1f);
        _ = khuAnimated.transform.DOScale(1.6f, 0.6f).From(1f)
            .SetEase(Ease.OutCubic);

        await UniTask.Delay(150);

        // MWE1
        mweStatic.SetActive(true);
        mweAnimated1.gameObject.SetActive(true);
        _ = mweAnimated1.DOFade(0, 0.6f).From(1f);
        _ = mweAnimated1.transform.DOScale(1.6f, 0.6f).From(1f)
            .SetEase(Ease.OutCubic);

        await UniTask.Delay(370);

        // MWE2
        mweAnimated2.gameObject.SetActive(true);
        _ = mweAnimated2.DOFade(0, 0.6f).From(1f);
        _ = mweAnimated2.transform.DOScale(1.5f, 0.8f).From(1f)
            .SetEase(Ease.OutCubic);

        await UniTask.Delay(380);

        // MWE3
        mweAnimated3.gameObject.SetActive(true);
        _ = mweAnimated3.DOFade(0, 0.6f).From(1f);
        _ = mweAnimated3.transform.DOScale(1.4f, 1f).From(1f)
            .SetEase(Ease.OutCubic);

        // At the end, set only the full text active
        // so it can be animated on game start.
        buStatic.SetActive(false);
        khuStatic.SetActive(false);
        mweStatic.SetActive(false);

        bukhumweText.gameObject.SetActive(true);
        bukhumweText.transform.localScale = Vector3.one;
        bukhumweText.alpha = 1f;
    }

    private void hide()
    {
        buStatic.SetActive(false);
        buAnimated.gameObject.SetActive(false);
        khuStatic.SetActive(false);
        khuAnimated.gameObject.SetActive(false);
        mweStatic.SetActive(false);
        mweAnimated1.gameObject.SetActive(false);
        mweAnimated2.gameObject.SetActive(false);
        mweAnimated3.gameObject.SetActive(false);
    }

    [NaughtyAttributes.Button]
    private void debugHide() => hide();

    [NaughtyAttributes.Button]
    private async void debugGameOver()
    {
        await PlayGameOverAsync();
    }
}
