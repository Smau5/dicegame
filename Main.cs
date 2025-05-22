using Godot;
using System;

public partial class Main : Node2D
{
    private PackedScene BattleScene = GD.Load<PackedScene>("res://Battle.tscn");
    private PackedScene LobbyScene = GD.Load<PackedScene>("res://lobby.tscn");
    public int Score = 0;
    public int PlayerDicesCount = 3;

    private PlayerField PlayerField = null;

    public override void _Ready()
    {
        PlayerField = GetNode<PlayerField>("PlayerField");
        PlayerField.InitializeDices(PlayerDicesCount);

    }


}
