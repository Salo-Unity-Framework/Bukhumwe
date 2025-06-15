using TMPro;
using UnityEngine;

/// <summary>
/// Part of the TitleUI prefab, this updates scores on TitleScene
/// </summary>
public class TitleScoreUIHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreTextUI;
    [SerializeField] private TMP_Text lastScoreTextUI;

    private void Start()
    {
        var playerStatsRuntimeData = AppSOHolder.Instance.PlayerStatsRuntimeData;

        highScoreTextUI.text = playerStatsRuntimeData.HighScore.ToString();
        lastScoreTextUI.text = playerStatsRuntimeData.LastScore.ToString();
    }
}
