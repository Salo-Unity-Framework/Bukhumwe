# Bukhumwe

Bukhumwe is a super-short fast-paced click fest game where you try to score as high as you can before your health runs out.

---

# Salo framework showcase

This project is an example implementation of the Salo Unity framework. In practice, the framework is not recommended for a small project like this and some of the framework feature usages may seem overkill, but they are meant as showcases.

Link to the Salo manual: https://www.notion.so/Salo-Manual-1d15eb3be3278058876cd7eb3bf055b4

Here are some highlights of the features in use:

### Bootstrapped scene
`BootstrapScene` is the scene that remains loaded throughout the game, with other scenes loaded addittively on top of it. Game systems used throughout the game are kept here. For example:
- The common background images and visuals
- The audio system
- The scene fade system
- The game's data holder (more on this later)

This eliminates the need for `DontDestroyOnLoad`s and Singletons.

Additionally on the Editor, these systems are always available whether you Editor-Play the title scene or the gameplay scene - the framework simulates build behaviour by loading in `BootstrapScene` first. You don't need to play through the game just to play your current scene with the correct setup.

### Runtime data
ScriptableObject assets derived from the framework's `RuntimeDataSOBase` class are used to pass data around among multiple components. These assets are in the `Assets/App/Data/AppData` folder.

For example, the `PlayerStatsRuntimeData` asset has a `LastScore` field that is updated by one system and is accessed by other totally separate systems. All decoupled, and this works across separate scenes.

The values assigned during Editor-Play are discarded so they don't pollute source control.

### Config data

Similar to RuntimeData, assets derived from the framework's `ConfigSOBase` class store edit-time configuration data. These are meant to store configuration values in assets rather than on the Editor inspector.

### Data holder
`AppSOHolder.cs` is a bootstrapped class that holds references to the RuntimeData and Config ScriptableObject assets in the project. This provides easy static access to these assets. For example:

```
scoreTextUI.text = AppSOHolder.Instance.RunRuntimeData.Score.ToString();
```

### Scene loading system
`TitleScene` is assigned to the `FirstScene` field on the `SceneLoadConfig` asset - this makes it the scene to be loaded on top of `BootstrapScene` when the game starts. (Note that the `SceneReference` type allows Scene assets to be assigned in the Editor inspector as long as they are Addressable).

Changing scenes is done by calling `SceneLoadEvents.MajorSceneLoadRequested(sceneToLoad)`. This will unload the current scene and load in the requested one.

### Static C# events
The framework and the Bukhumwe project use static C# events extensively to decouple logic flow. Static slasses ending with "Events.cs" hold these events in the project. For example, `RunEvents.cs` holds events like `OnEnemyHit`, `OnScoreUpdated`, `OnPlayerDeath`, and so on.

### Resource loading
The framework provides a controlled resource loading system that ties in with the scene loading system.

For example on `GameplayScene`, `GameplayResourceLoader.cs` loads the Addressable Enemy asset (among other things). `EnemyPooler.cs` then pre-warms a pooler as part of loading. The `SceneLoadEvents.OnSceneReady` event is invoked by the framework when all loaders in the scene have completed. Other systems listen to this event, like the scene fader to start a fade in.

Note that the pool pre-warming is spread across multiple frames to avoid fps drops during loading. The framework processes registered loaders sequentially to accommodate processes like this.

### Static instance references
`StaticInstanceOf<T>` is a class on the framework that provides a more flexible alternative to `DontDestroyOnLoad`s and singletons.

Sometimes, the framework's data system or the static C# events may not be sufficient when you want a decoupled setup without explicit component references. Like when you need a return value from a method call, or when the call needs to be awaited (these can still be accomplised with events but things become complex fast). Here are some examples below:

##### As a `DontDestroyOnLoad` alternative
`SfxPlayer.cs` is placed in `BootstrapScene` and therefore can be called from anywhere in the project to play audio:
```
SfxPlayer.Instance.PlayOneShot(gameOverClip);
```

##### As a singleton alternative
`EnemyPooler.cs` is a script on `GameplayScene` that can be accessed like so:
```
var enemy = EnemyPooler.Instance.Get();
```

##### As a static awaitable
`PlayerDeathHandler.cs` has an async method that animates text on game over. `RunManager.cs` calls and awaits this method (without needing a reference) before loading the title scene:
```
await PlayerDeathHandler.Instance.HandlePlayerDeathAsync();
SceneLoadEvents.TitleSceneLoadRequested();
```

### Data persistence
`PlayerStatsRuntimeDataSO.cs` holds the only persisted data in the game, for high score and last score.

Implementing data persistence is easy: Mark the field to persist with the `[Persisted]` attribute, and add the asset to the list on the `DataPersistenceConfig` asset. That's it! Just call `Save()` on the class to write to disk. The framework will automatically load the value during bootstrap loading.

The framework's `PlayerPrefsPersistor` asset is assigned to `DataPersistenceConfig` so saved data will be written to and read from Unity's PlayerPrefs. You can write your own persistor by subclassing `DataPersistorSOBase`, like an encrypted version of `PlayerPrefsPersistor` to prevent players from modifying save data.

### Scene fade
`SceneFader.cs` is a subclass of `SceneFaderBase.cs`. Placing it anywhere in `BootstrapScene`, the framework will pick it up and use it for scene fading throughout the game.

The script overrides the `FadeOut` method to implement a fade out to black. To implement a fade in, the script listens to the `SceneLoadEvents.OnSceneReady` event to fade back in.

---

# License
This example project is licensed under the MIT License. You're free to copy, modify, and use the code as long as you include the license.

Note: This project depends on the Salo Unity package, which is **not included** and is subject to a separate license. To access or use the package, please contact: vanlalhriata@gmail.com.

---
Fun fact: The sound that asteroids make when they explode in space is "BU-KHU-MWE".