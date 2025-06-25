using Cysharp.Threading.Tasks;
using Salo.Infrastructure;
using UnityEngine;

/// <summary>
/// Part of GameplayRun prefab, this plays effects on Player death.
/// This is an example of StaticInstanceOf in use.
/// </summary>
public class PlayerDeathHandler : StaticInstanceOf<PlayerDeathHandler>
{
    [SerializeField] private RunConfigSO runConfig;

    /// <summary>
    /// Called and awaited by RunManager. This is an example of when a C# event
    /// is not sufficient. Like when return values are expected, or the call
    /// needs to be awaited. Alternately RunManager could hold a reference
    /// to this instance, but thos takes the StaticInstanceOf approach.
    /// </summary>
    public async UniTask HandlePlayerDeathAsync()
    {
        // On enemy death, delay before starting effects
        await UniTask.Delay(runConfig.GameOverPauseMilliseconds);

        // Play death SFX
        SfxPlayer.Instance.PlayOneShot(runConfig.GameOverClip);

        await BukhumweController.Instance.PlayGameOverAsync();
    }
}
