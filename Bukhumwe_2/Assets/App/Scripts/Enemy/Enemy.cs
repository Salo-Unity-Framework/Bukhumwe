using UnityEngine;

/// <summary>
/// The base class for all enemies and is on the EnemyBase prefab (and its variants).
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider2D hitCollider;
    public Collider2D HitCollider => hitCollider;

    // Should be set on spawn by EnemySpawner
    public float Speed;
    public Vector3 NormalizedDirection;

    private void Update()
    {
        move();
    }

    private void move()
    {
        // Move in Play state only
        if (AppSOHolder.Instance.RunRuntimeData.CurrentRunState != RunState.Play) return;

        transform.position += Time.deltaTime * Speed * NormalizedDirection;
    }

    // The enemy was hit
    public virtual void ProcessHit()
    {
        // TODO: Deactivate colliders

        RunEvents.EnemyHit(this);

        // TODO: Death animation

        EnemyPooler.Instance.Release(this);
    }
}
