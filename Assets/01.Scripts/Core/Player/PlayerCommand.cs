public enum PlayerCommandType
{
    None,
    Jump,
}

public struct PlayerCommand
{
    public PlayerCommandType type;
    public float time;

    public PlayerCommand(PlayerCommandType type, float time)
    {
        this.type = type;
        this.time = time;
    }
}