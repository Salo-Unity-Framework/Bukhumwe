using DG.Tweening;
using Salo.Infrastructure;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Part of the TitleSceneManager prefab, this handles the
/// enemy on the title scene used to start gameplay.
/// </summary>
public class TitleEnemyHandler : MonoBehaviour
{
    [SerializeField] private RunConfigSO runConfig; // For enemy layer

    private Enemy loadedEnemyPrefab; // loaded by TitleResourceLoader
    private Camera mainCamera; // Assigned on Awake

    private Enemy instantiatedEnemy;

    // Check if the player clicks on the single enemy.
    // True if waiting for input after spawn.
    private bool shouldCheckPlayerInput = false;

    private void OnEnable()
    {
        SceneLoadEvents.OnSceneReady += handleSceneReady;
        RunEvents.OnEnemyReleaseReady += handleEnemyReleaseReady;
    }

    private void OnDisable()
    {
        SceneLoadEvents.OnSceneReady -= handleSceneReady;
        RunEvents.OnEnemyReleaseReady -= handleEnemyReleaseReady;
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (shouldCheckPlayerInput) checkPlayerInput();
    }

    public void SetLoadedEnemyPrefab(Enemy enemy)
    {
        loadedEnemyPrefab = enemy;
    }

    private void handleSceneReady()
    {
        Assert.IsNotNull(loadedEnemyPrefab);

        // Instantiate the loaded enemy and animate it in
        instantiatedEnemy = Instantiate(loadedEnemyPrefab);
        instantiatedEnemy.transform.DOScale(1, 1f)
            .From(0).SetEase(Ease.InCubic);

        shouldCheckPlayerInput = true;
    }

    private void handleEnemyReleaseReady(Enemy _)
    {
        // Still assuming this is instantiatedEnemy. Destroy manually since it
        // was instantiated here and is not part of GameplayScene's pool.
        Destroy(instantiatedEnemy.gameObject);
        instantiatedEnemy = null;
    }

    private void checkPlayerInput()
    {
        // Ignore if no input this frame. This works for touch too
        if (!Input.GetMouseButtonDown(0)) return;

        // Raycast at the mouse/touch position
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var colliderHit = Physics2D.OverlapPoint(mousePosition, 1 << runConfig.EnemyLayer);

        if (null == colliderHit) return; // No valid hit

        // Since there is only one enemy on the title scene, assume it was the one hit
        instantiatedEnemy.ProcessHit();

        // Easy static call without needing an AudioSource since this is a one time call. Note that
        // this creates and destroys an object - it should not be used for frequent calls.
        AudioSource.PlayClipAtPoint(runConfig.EnemyHitClip, mainCamera.transform.position);
    }
}
