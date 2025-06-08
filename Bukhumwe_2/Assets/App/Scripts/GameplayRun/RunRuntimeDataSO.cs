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

    [Tooltip("Screen extents set at start of gameplay scene. The viewport extents by x and y from the center")]
    public Vector2 ViewportExtent;

    [Tooltip("The Enemy prefab loaded from the Addressable asset during scene resource loading")]
    public Enemy LoadedEnemyPrefab;

    [Tooltip("The current gameplay run state")]
    public RunState CurrentRunState;
}
