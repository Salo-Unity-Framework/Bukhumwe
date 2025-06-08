using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Part of the GameplayRun prefab, this detects player input during a run
/// </summary>
public class RunInputManager : MonoBehaviour
{
    [SerializeField] private RunRuntimeDataSO runRuntimeData;

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
        var raycastHit = Physics2D.Raycast(mousePosition, Vector2.zero, mainCamera.farClipPlane);
        // TODO: Create Enemy layer, assign in RunConfig, and filter raycast

        if (null == raycastHit.collider) return; // No valid hit

        // Check if the hit collider is a registered enemy
        if (runRuntimeData.ActiveEnemies.TryGetValue(raycastHit.collider, out var enemy))
        {
            // The enemy instance was hit
            enemy.ProcessHit();
        }
    }
}
