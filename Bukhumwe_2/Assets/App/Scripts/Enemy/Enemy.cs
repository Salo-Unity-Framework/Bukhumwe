using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// The base class for all enemies and is on the EnemyBase prefab (and its variants).
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("References")]

    // Use Rigidbody for proper collisions and triggers
    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private Collider2D hitCollider;
    public Collider2D HitCollider => hitCollider;

    [SerializeField] private Transform visuals;
    [SerializeField] private GameObject aliveVisuals;
    [SerializeField] private GameObject deathVisuals;

    [Header("Runtime data")]

    // Should be set on spawn by EnemySpawner
    public float Speed;
    public Vector3 NormalizedDirection;

    private void Start()
    {
        // Rotate visuals so flames face along the direction of travel
        visuals.up = -NormalizedDirection;
    }

    private void FixedUpdate()
    {
        move();
    }

    public void Reset()
    {
        toggleVisualsAndCollider(isAlive: true);
    }

    private void move()
    {
        // Move in Play state only
        if (AppSOHolder.Instance.RunRuntimeData.CurrentRunState != RunState.Play) return;

        var newPosition = transform.position + Time.deltaTime * Speed * NormalizedDirection;
        rigidbody.MovePosition(newPosition);
    }

    // The enemy was hit
    public virtual void ProcessHit()
    {
        RunEvents.EnemyHit(this);
        destroy().Forget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore if this didn't collide with the Player base
        if (collision.gameObject.layer != AppSOHolder.Instance.RunConfig.PlayerLayer) return;

        // The enemy has hit the player base. PlayerBase
        // will process its own trigger event.

        destroy().Forget();
    }

    private void toggleVisualsAndCollider(bool isAlive)
    {
        hitCollider.enabled |= isAlive;
        aliveVisuals.SetActive(isAlive);
        deathVisuals.SetActive(!isAlive);
    }

    private async UniTaskVoid destroy()
    {
        toggleVisualsAndCollider(isAlive: false);

        // HACK: Hard code the wait time. This should be the explosion animation time
        await UniTask.Delay(420);

        deathVisuals.SetActive(false);

        // Avoiding releasing to pool here since the enemy may be spawned without
        // a pooler. Let the spawner that used pooling handle the release.
        RunEvents.EnemyReleaseReady(this);
    }
}
