using Salo.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The runtime data for a single Run
/// </summary>
[CreateAssetMenu(fileName = "RunRuntimeData", menuName = "Bukhumwe/Run RuntimeData")]
public class RunRuntimeDataSO : RuntimeDataSOBase
{
    public Dictionary<Collider2D, Enemy> ActiveEnemies = new();
}
