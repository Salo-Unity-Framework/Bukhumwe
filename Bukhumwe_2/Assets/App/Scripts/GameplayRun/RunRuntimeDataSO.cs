using Salo.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The runtime data for a single Run
/// </summary>
[CreateAssetMenu(fileName = "RunRuntimeData", menuName = "Bukhumwe/AppData/Run RuntimeData")]
public class RunRuntimeDataSO : RuntimeDataSOBase
{
    public Dictionary<Collider2D, Enemy> ActiveEnemies = new();

    [Tooltip("The Enemy prefab loaded from the Addressable asset during scene resource loading")]
    public Enemy LoadedEnemyPrefab;
}
