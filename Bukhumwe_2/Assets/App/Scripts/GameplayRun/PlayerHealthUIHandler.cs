using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Part of the GameplayUI prefab, this updates the player health bar
/// </summary>
public class PlayerHealthUIHandler : MonoBehaviour
{
    [SerializeField] private Image healthBarImage;

    private void OnEnable()
    {
        RunEvents.OnRunStateChanged += handleRunStateChanged;
        RunEvents.OnHealthUpdated += handleHealthUpdated;
    }

    private void OnDisable()
    {
        RunEvents.OnRunStateChanged -= handleRunStateChanged;
        RunEvents.OnHealthUpdated -= handleHealthUpdated;
    }

    private void handleRunStateChanged(RunState _, RunState newState)
    {
        switch (newState)
        {
            case RunState.Intro:
                healthBarImage.DOFade(1, 1f).From(0).SetDelay(1f);
                break;
        }
    }

    private void handleHealthUpdated(int _, int updatedHealth)
    {
        var fillPercentage = (float)updatedHealth / AppSOHolder.Instance.RunConfig.StartintPlayerHealth;
        fillPercentage = Mathf.Clamp01(fillPercentage); // Don't go beyond [0,1]

        healthBarImage.fillAmount = fillPercentage;
    }
}
