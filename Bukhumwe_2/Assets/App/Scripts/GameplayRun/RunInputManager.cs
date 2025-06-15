using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Part of the GameplayRun prefab, this detects player input during a run
/// </summary>
public class RunInputManager : MonoBehaviour
{
    [SerializeField] private RunRuntimeDataSO runRuntimeData;
    [SerializeField] private RunConfigSO runConfig;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        Assert.IsNotNull(mainCamera);
    }

    private void Update()
    {
        checkPlayerInput();
    }

    private void checkPlayerInput()
    {
        if (runRuntimeData.CurrentRunState != RunState.Play) return;

        // Ignore if no input this frame. This works for touch too
        if (!Input.GetMouseButtonDown(0)) return;
        
        // Raycast at the mouse/touch position
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var colliderHit = Physics2D.OverlapPoint(mousePosition, 1 << runConfig.EnemyLayer);

        if (null == colliderHit) return; // No valid hit

        // Check if the hit collider is a registered enemy
        if (runRuntimeData.ActiveEnemies.TryGetValue(colliderHit, out var enemy))
        {
            // The enemy instance was hit
            enemy.ProcessHit();
        }
    }
}
