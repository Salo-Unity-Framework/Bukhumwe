using Salo.Infrastructure;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The runtime data for a single Run
/// </summary>
[CreateAssetMenu(fileName = "RunRuntimeData", menuName = "Bukhumwe/AppData/Run RuntimeData")]
public class RunRuntimeDataSO : RuntimeDataSOBase
{
    private Dictionary<Collider2D, Enemy> activeEnemies;
    public Dictionary<Collider2D, Enemy> ActiveEnemies
    {
        get
        {
            if (null == activeEnemies) activeEnemies = new();
            return activeEnemies;
        }
    }

    [Tooltip("The Enemy prefab loaded from the Addressable asset during scene resource loading")]
    public Enemy LoadedEnemyPrefab;
}
