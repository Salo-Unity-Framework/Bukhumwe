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
        RunEvents.OnHealthUpdated += handleHealthUpdated;
    }

    private void OnDisable()
    {
        RunEvents.OnHealthUpdated -= handleHealthUpdated;
    }

    private void handleHealthUpdated(int _, int updatedHealth)
    {
        var fillPercentage = (float)updatedHealth / AppSOHolder.Instance.RunConfig.StartintPlayerHealth;
        fillPercentage = Mathf.Clamp01(fillPercentage); // Don't go beyond [0,1]

        healthBarImage.fillAmount = fillPercentage;
    }
}
