using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Text;
using TMPro;
using UnityEngine;

/// <summary>
/// Script on the ScorePopup prefab
/// </summary>
public class ScorePopup : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTextUI;

    private static StringBuilder stringBuilder = new StringBuilder();

    private const float Y_AMOUNT = 1f; // Amount to move up
    private const float DURATION_SECONDS = 1f;

    // The offset from the enemy position to spawn the popup
    private static readonly Vector3 SPAWN_OFFSET = new Vector3(0, 0.25f, 0);

    /// <summary>
    /// Show the score and then wait to be released
    /// </summary>
    public async UniTask Show(int score, Vector3 enemyPosition)
    {
        stringBuilder.Clear();
        stringBuilder.Append('+');
        stringBuilder.Append(score);
        scoreTextUI.text = stringBuilder.ToString();

        transform.position = enemyPosition + SPAWN_OFFSET;

        // Animate
        _ = scoreTextUI.DOFade(0, DURATION_SECONDS).From(1f);
        _ = transform.DOLocalMoveY(transform.position.y + Y_AMOUNT, DURATION_SECONDS);
        await scoreTextUI.transform.DOScale(1.2f, DURATION_SECONDS)
            .From(1f) .SetEase(Ease.InCubic);
    }

    [NaughtyAttributes.Button]
    private void debugShow()
    {
        Show(18, Vector3.zero);
    }
}
