using Godot;
using System;

public partial class Lives : Label
{
    [Export]
    PlayerStats playerStats = null;

    public override void _Ready()
    {
        Text = playerStats.Lives.ToString();
    }


}
