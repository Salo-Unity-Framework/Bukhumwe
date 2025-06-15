using TMPro;
using UnityEngine;

/// <summary>
/// Part of the GameplayUI prefab, this updates the score UI
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
            spawnScoreIndicator(eventArgs.scoreDelta, eventArgs.scorePosition);
        }
    }

    private void spawnScoreIndicator(int scoreDelta, Vector3 scorePosition)
    {
        // TODO
    }
}
