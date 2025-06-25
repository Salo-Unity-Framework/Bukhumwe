using Salo.Infrastructure;
using UnityEngine;

/// <summary>
/// This is a bootstrapped StaticInstanceOf on the Audio prefab.
/// This uses a single AudioSource to play SFX.
/// </summary>
public class SfxPlayer : StaticInstanceOf<SfxPlayer>
{
    [SerializeField] private AudioSource audioSource;

    public void PlayOneShot(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
