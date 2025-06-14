using Godot;

[GlobalClass]
public partial class PlayerStats : Resource
{
    [Export]
    private int _lives = 0;
    [Export]
    private int _gold = 0;
    [Export]
    public Godot.Collections.Array<DiceStats> dices = new Godot.Collections.Array<DiceStats>();

    [Signal]
    public delegate void PlayerStatsChangedChangedEventHandler();

    public int Lives
    {
        get
        {
            return _lives;
        }
        set
        {
            _lives = value;
            EmitSignal(SignalName.PlayerStatsChangedChanged);
        }
    }

    public int Gold
    {
        get
        {
            return _gold;
        }
        set
        {
            _gold = value;
            EmitSignal(SignalName.PlayerStatsChangedChanged);
        }
    }
}

