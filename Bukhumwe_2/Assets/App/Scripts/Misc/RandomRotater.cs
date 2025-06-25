using UnityEngine;

/// <summary>
/// Randomly sets a rotation along the z-axis.
/// Used by EnemyBase, for one.
/// </summary>
public class RandomRotater : MonoBehaviour
{
    private const float MINIMUM_SPEED = 20f;
    private const float MAXIMUM_SPEED = 100f;

    private float rotationSpeed = 1f;

    private void Awake()
    {
        int direction = Random.value > 0.5f ? 1 : -1; // Clockwise or anti
        rotationSpeed = direction * Random.Range(MINIMUM_SPEED, MAXIMUM_SPEED);
    }

    private void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * rotationSpeed);
    }
}
