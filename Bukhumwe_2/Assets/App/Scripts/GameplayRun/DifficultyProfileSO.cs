using UnityEngine;

/// <summary>
/// This represents a difficulty profile. Multiple profiles
/// can be created but only a single one will be in use.
/// Asset to be assigned to RunConfig.
/// </summary>
[CreateAssetMenu(fileName = "DifficultyProfile", menuName = "Bukhumwe/Data/Difficulty Profile")]
public class DifficultyProfileSO : ScriptableObject
{
    [Header("Enemy Spawn interval")]

    [Tooltip("The base enemy spawn interval at the start of the game")]
    [SerializeField] private float spawnIntervalAtStartSeconds;
    public float SpawnIntervalAtStartSeconds => spawnIntervalAtStartSeconds;

    [Tooltip("The enemy spawn interval at maximum difficulty")]
    [SerializeField] private float spawnIntervalAtEndSeconds;
    public float SpawnIntervalAtEndSeconds => spawnIntervalAtEndSeconds;

    [Tooltip("The run time at which spawn interval will reach maximum difficulty")]
    [SerializeField] private float spawnIntervalMaxDiffSeconds;
    public float SpawnIntervalMaxDiffSeconds => spawnIntervalMaxDiffSeconds;

    [Tooltip("The curve at which to increase spawn interval difficulty")]
    [SerializeField] private AnimationCurve spawnIntervalCurve;
    public AnimationCurve SpawnIntervalCurve => spawnIntervalCurve;

    [Tooltip("Factor to randomize spawn interval. Eg: Value of 0.3 will randomize" +
        "spawn interval of 2 between 1.4 and 2.6 (0.3 * 2 on both sides of 2)")]
    [SerializeField] private float spawnIntervalSigma;
    public float SpawnIntervalSigma => spawnIntervalSigma;
}
