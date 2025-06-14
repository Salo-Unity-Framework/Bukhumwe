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

    [Tooltip("This is multiplied with an enemy's speed to get the score")]
    [SerializeField] private float enemySpeedToScore;
    public float EnemySpeedToScore => enemySpeedToScore;

    [Tooltip("The player layer for easy reference")]
    [SerializeField, NaughtyAttributes.Layer] private int playerLayer;
    public int PlayerLayer => playerLayer;

    [Tooltip("The enemy layer for easy reference")]
    [SerializeField, NaughtyAttributes.Layer] private int enemyLayer;
    public int EnemyLayer => enemyLayer;
}
