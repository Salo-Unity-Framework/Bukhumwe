using Salo.Infrastructure;
using UnityEngine;

[CreateAssetMenu(fileName = "RunConfig", menuName = "Bukhumwe/AppData/Run Config")]
public class RunConfigSO : ConfigSOBase
{
    [SerializeField] private ComponentReferenceEnemy enemyPrefabReference;
    public ComponentReferenceEnemy EnemyPrefabReference => enemyPrefabReference;
}
