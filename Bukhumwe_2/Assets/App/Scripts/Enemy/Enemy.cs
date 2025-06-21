using UnityEngine;

/// <summary>
/// The base class for all enemies and is on the EnemyBase prefab (and its variants).
/// </summary>
public class Enemy : MonoBehaviour
{
    // Use Rigidbody for proper collisions and triggers
    [SerializeField] private new Rigidbody2D rigidbody;

    [SerializeField] private Collider2D hitCollider;
    public Collider2D HitCollider => hitCollider;

    [SerializeField] private Transform visuals;

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
        destroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore if this didn't collide with the Player base
        if (collision.gameObject.layer != AppSOHolder.Instance.RunConfig.PlayerLayer) return;

        // The enemy has hit the player base. PlayerBase
        // will process its own trigger event.

        destroy();
    }

    private void destroy()
    {
        // TODO: Deactivate colliders
        // TODO: Explosion animation

        // Avoiding releasing to pool here since the enemy may be spawned without
        // a pooler. Let the spawner that used pooling handle the release.
        RunEvents.EnemyReleaseReady(this);
    }
}
