using UnityEngine;

/// <summary>
/// Part of the PlayerBase prefab, handles interactions
/// with the Player base object
/// </summary>
public class PlayerBase : MonoBehaviour
{
    // NOTE: The camera's vertical viewport is preserved across
    // different aspect ratios. Placing the base correctly
    // at the bottom during edit time should work.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Ignore if the collided object was not an Enemy
        if (collision.gameObject.layer != AppSOHolder.Instance.RunConfig.EnemyLayer) return;

        // TODO: Implement player health and game over
    }
}
