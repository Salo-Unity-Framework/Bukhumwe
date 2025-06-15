using Salo.Infrastructure;
using UnityEngine;

/// <summary>
/// This bootstrapped script holds references to the project's AppData
/// SO assets so they can be accessed from a static context
/// </summary>
public class AppSOHolder : StaticInstanceOf<AppSOHolder>
{
    [Header("Runtime Data")]

    [SerializeField] private RunRuntimeDataSO runRuntimeData;
    public RunRuntimeDataSO RunRuntimeData => runRuntimeData;

    [SerializeField] private PlayerStatsRuntimeDataSO playerStatsRuntimeData;
    public PlayerStatsRuntimeDataSO PlayerStatsRuntimeData => playerStatsRuntimeData;

    [Header("Config")]

    [SerializeField] private RunConfigSO runConfig;
    public RunConfigSO RunConfig => runConfig;
}
