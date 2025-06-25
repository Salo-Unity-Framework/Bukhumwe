using Salo.Infrastructure;
using UnityEngine;

[CreateAssetMenu(fileName = "RunConfig", menuName = "Bukhumwe/AppData/Run Config")]
public class RunConfigSO : ConfigSOBase
{
    [Header("Setup")]

    [Tooltip("Time to wait before starting gameplay")]
    [SerializeField] private int runIntroMilliseconds;
    public int RunIntroMilliseconds => runIntroMilliseconds;

    [Tooltip("Time to wait on game over before starting game over effects")]
    [SerializeField] private int gameOverPauseMilliseconds;
    public int GameOverPauseMilliseconds => gameOverPauseMilliseconds;

    [Tooltip("The Addressable enemy prefab")]
    [SerializeField] private ComponentReferenceEnemy enemyPrefabReference;
    public ComponentReferenceEnemy EnemyPrefabReference => enemyPrefabReference;

    [Tooltip("The player layer for easy reference")]
    [SerializeField, NaughtyAttributes.Layer] private int playerLayer;
    public int PlayerLayer => playerLayer;

    [Tooltip("The enemy layer for easy reference")]
    [SerializeField, NaughtyAttributes.Layer] private int enemyLayer;
    public int EnemyLayer => enemyLayer;

    [Tooltip("The difficulty profile set for the game")]
    [SerializeField] private DifficultyProfileSO difficultyProfile;
    public DifficultyProfileSO DifficultyProfile => difficultyProfile;

    [Header("Base values")]

    [Tooltip("The health the player starts with")]
    [SerializeField] private int startingPlayerHealth;
    public int StartintPlayerHealth => startingPlayerHealth;

    [Tooltip("The damage an enemy causes to player's health")]
    [SerializeField] private int enemyDamage;
    public int EnemyDamage => enemyDamage;

    [Tooltip("This is multiplied with an enemy's speed to get the score")]
    [SerializeField] private float enemySpeedToScore;
    public float EnemySpeedToScore => enemySpeedToScore;

    [Header("Audio")]

    [Tooltip("The SFX to play on enemy hit")]
    [SerializeField] private AudioClip enemyHitClip;
    public AudioClip EnemyHitClip => enemyHitClip;

    [Tooltip("The SFX to play on plyer hit")]
    [SerializeField] private AudioClip playerHitClip;
    public AudioClip PlayerHitClip => playerHitClip;

    [Tooltip("The SFX to play on game over")]
    [SerializeField] private AudioClip gameOverClip;
    public AudioClip GameOverClip => gameOverClip;
}
