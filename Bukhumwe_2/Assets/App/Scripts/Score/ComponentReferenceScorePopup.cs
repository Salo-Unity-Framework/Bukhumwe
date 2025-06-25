using System;

/// <summary>
/// Similar to ComponentReferenceEnemy
/// </summary>
[Serializable]
public class ComponentReferenceScorePopup : ComponentReference<ScorePopup>
{
    public ComponentReferenceScorePopup(string guid) : base(guid) { }
}
