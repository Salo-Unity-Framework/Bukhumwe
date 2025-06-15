using Salo.Infrastructure;
using UnityEngine;

/// <summary>
/// Persisted player stats data
/// </summary>
[CreateAssetMenu(fileName = "PlayerStatsRuntimeData", menuName = "Bukhumwe/AppData/PlayerStats RuntimeData")]
public class PlayerStatsRuntimeDataSO : RuntimeDataSOBase, IPersistable
{
    [Tooltip("The high score saved on this device")]
    [Persisted] public int HighScore;

    [Tooltip("The score of the last run")]
    [Persisted] public int LastScore;
}
