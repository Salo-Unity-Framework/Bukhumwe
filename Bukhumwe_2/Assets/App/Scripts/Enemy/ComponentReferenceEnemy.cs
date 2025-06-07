using System;

/// <summary>
/// An AssetReference to hold Addressable Enemy prefabs. Alternately, a simpler approach
/// would have been to use AssetReferenceGameObject directly, but that gives you a
/// GameObject instead of the ComponentReference's component, like Enemy in this
/// case. Plus this is more robust, even if it requires more setup.
/// </summary>
[Serializable]
public class ComponentReferenceEnemy : ComponentReference<Enemy>
{
    public ComponentReferenceEnemy(string guid) : base(guid) { }
}
