using Salo.Infrastructure;
using UnityEngine;

[CreateAssetMenu(fileName = "AppFlowConfig", menuName = "Bukhumwe/AppData/AppFlow Config")]
public class AppFlowConfigSO : ScriptableObject
{
    [Tooltip("The gameplay scene")]
    [SerializeField] private SceneReference gameplayScene;
    public SceneReference GameplayScene => gameplayScene;

    [Tooltip("Time to wait after click to load the gameplay scene")]
    [SerializeField] private int gameplayLoadDelayMilliseconds;
    public int GameplayLoadDelayMilliseconds => gameplayLoadDelayMilliseconds;

    [Tooltip("Scene fade out duration")]
    [SerializeField] private float sceneFadeOutSeconds;
    public float SceneFadeOutSeconds => sceneFadeOutSeconds;

    [Tooltip("Scene fade in duration")]
    [SerializeField] private float sceneFadeInSeconds;
    public float SceneFadeInSeconds => sceneFadeInSeconds;
}
