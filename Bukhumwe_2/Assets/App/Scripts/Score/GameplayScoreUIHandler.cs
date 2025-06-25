using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

/// <summary>
/// Part of the GameplayUI prefab, this updates the score UI
/// and spawns score popups
/// </summary>
public class GameplayScoreUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTextUI;

    private void OnEnable()
    {
        RunEvents.OnScoreUdpated += handleScoreUdpated;
    }

    private void OnDisable()
    {
        RunEvents.OnScoreUdpated -= handleScoreUdpated;
    }

    private void handleScoreUdpated(RunEvents.ScoreEventArgs eventArgs)
    {
        scoreTextUI.text = eventArgs.updatedScore.ToString();

        // Spawn score indication if non-zero delta. Zero delta is for reset etc
        if (eventArgs.scoreDelta != 0)
        {
            spawnScoreIndicator(eventArgs.scoreDelta, eventArgs.scorePosition).Forget();
        }
    }

    private async UniTaskVoid spawnScoreIndicator(int scoreDelta, Vector3 scorePosition)
    {
        var scorePopup = ScorePopupPooler.Instance.Get();

        await scorePopup.Show(scoreDelta, scorePosition);

        // Release back to pool after it finishes animating
        ScorePopupPooler.Instance.Release(scorePopup);
    }
}
