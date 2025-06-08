using Salo.Infrastructure;
using UnityEngine;

[CreateAssetMenu(fileName = "RunConfig", menuName = "Bukhumwe/AppData/Run Config")]
public class RunConfigSO : ConfigSOBase
{
    [Tooltip("Time to wait before starting gameplay")]
    [SerializeField] private int runIntroMilliseconds;
    public int RunIntroMilliseconds => runIntroMilliseconds;

    [Tooltip("The Addressable enemy prefab")]
    [SerializeField] private ComponentReferenceEnemy enemyPrefabReference;
    public ComponentReferenceEnemy EnemyPrefabReference => enemyPrefabReference;
}
