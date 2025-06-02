using UnityEngine;

/// <summary>
/// The base class for all enemies and is on the EnemyBase prefab (and its variants).
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] private Collider2D hitCollider;

    protected virtual void OnEnable()
    {
        var runRuntimeData = AppSOHolder.Instance.RunRuntimeData;

        // Register on the active enemy list
        if (!runRuntimeData.ActiveEnemies.ContainsKey(hitCollider))
        {
            runRuntimeData.ActiveEnemies.Add(hitCollider, this);
        }
    }

    protected virtual void OnDisable()
    {
        var runRuntimeData = AppSOHolder.Instance.RunRuntimeData;
        runRuntimeData.ActiveEnemies.Remove(hitCollider);
    }

    // The enemy was hit
    public virtual void ProcessHit()
    {
        RunEvents.EnemyHit(this);
    }
}
