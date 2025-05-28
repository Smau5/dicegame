using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



public partial class Battle : Node2D
{
    [Signal]
    public delegate void EndedEventHandler(Battle battle);
    [Export]
    int NumberOfRounds = 3;
    [Export]
    int targetScore = 50;
    private PlayerField PlayerField = null;
    int score = 0;
    private PlayerScore PlayerScore = null;
    private TargetScore TargetScore = null;
    [Export]
    public PlayerStats PlayerStats = null;


    public override void _Ready()
    {
        GetViewport().PhysicsObjectPickingSort = true;
        GetViewport().PhysicsObjectPickingFirstOnly = true;


        PlayerField = GetNode<PlayerField>("PlayerField");
        PlayerScore = GetNode<PlayerScore>("PlayerScore");
        TargetScore = GetNode<TargetScore>("TargetScore");
        TargetScore.SetScore(targetScore);

        PlayerField.InitializeDices(PlayerStats.dices);

        // PlayDices 3 times with 1 second interval
        PlayDicesMultipleTimes(NumberOfRounds, 1.0f);

    }

    private async void PlayDicesMultipleTimes(int times, float intervalSeconds)
    {
        for (int i = 0; i < times; i++)
        {
            await PlayDices();
            await ToSignal(GetTree().CreateTimer(intervalSeconds), "timeout");
        }
    }


    public async Task PlayDices()
    {
        var dices = PlayerField.GetDices();

        PlayerField.RollAllDices();
        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        List<int> seenNumbers = new List<int>();
        foreach (var item in dices)
        {
            score += item.Value;
            seenNumbers.Add(item.Value);
            PlayerScore.SetScore(score);
            await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        }

    }


}
