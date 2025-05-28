using Godot;
using System;
using System.Collections.Generic;
using System.Linq;



public partial class Battle : Node2D
{
    [Signal]
    public delegate void EndedEventHandler(Battle battle);
    const int playerRerolls = 1;
    const int playerThrows = 3;
    int PlayerRerollRemaining = playerRerolls;
    int PlayerThrowsRemaining = playerThrows;
    private PlayerField PlayerField = null;
    private RerollSelectedButton RerollSelected = null;
    int score = 0;
    int targetScore = 50;
    private PlayerScore PlayerScore = null;
    private TargetScore TargetScore = null;
    [Export]
    public PlayerStats playerStats = null;

    public override void _Ready()
    {
        GetViewport().PhysicsObjectPickingSort = true;
        GetViewport().PhysicsObjectPickingFirstOnly = true;


        PlayerField = GetNode<PlayerField>("PlayerField");
        RerollSelected = GetNode<RerollSelectedButton>("RerollSelectedButton");
        PlayerScore = GetNode<PlayerScore>("PlayerScore");
        TargetScore = GetNode<TargetScore>("TargetScore");
        TargetScore.SetScore(targetScore);
        RerollSelected.SetLabel(PlayerRerollRemaining);
    }


    public void OnRerollSelectedPressed()
    {
        if (PlayerRerollRemaining > 0)
        {
            SetRemainingRerolls(PlayerRerollRemaining - 1);
            PlayerField.RerollSelectedDices();
        }
    }


    private void SetRemainingRerolls(int remainingRerolls)
    {
        PlayerRerollRemaining = remainingRerolls;
        RerollSelected.SetLabel(PlayerRerollRemaining);
    }

    public void ResetRerolls()
    {
        SetRemainingRerolls(playerRerolls);
    }

    public void ResetBattle()
    {
        PlayerField.SetAllDicesInitial();
        PlayerScore.SetScore(0);
        TargetScore.SetScore(50);
        score = 0;
        EmitSignal(SignalName.Ended);
    }


    public void PlayDices()
    {
        var dices = PlayerField.GetDices();

        PlayerField.RerollAllDices();
        List<int> seenNumbers = new List<int>();
        foreach (var item in dices)
        {
            int repeatedNumber = seenNumbers.Count(n => n == item.Value);
            score += item.Value * (repeatedNumber + 1);
            seenNumbers.Add(item.Value);
        }

        PlayerScore.SetScore(score);
    }


}
