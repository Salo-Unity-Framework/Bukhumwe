/// <summary>
/// The stage of a Gameplay run
/// </summary>
public enum RunState
{
    None,
    Intro,  // The time before actual gameplay starts
    Play,   // Normal gameplaay
    Outro,  // The player has died. Will go to title scene soon
}
