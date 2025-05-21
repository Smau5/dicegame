using Godot;
using System;
using System.Collections.Generic;
using System.Linq;



public partial class Battle : Node2D
{
    const int playerRerolls = 1;
    const int playerThrows = 3;
    int PlayerRerollRemaining = playerRerolls;
    int PlayerThrowsRemaining = playerThrows;
    private PlayerHand PlayerHand = null;
    private RerollSelectedButton RerollSelected = null;
    int score = 0;
    int targetScore = 50;
    private PlayerScore PlayerScore = null;

    public override void _Ready()
    {
        GetViewport().PhysicsObjectPickingSort = true;
        GetViewport().PhysicsObjectPickingFirstOnly = true;


        PlayerHand = GetNode<PlayerHand>("PlayerHand");
        RerollSelected = GetNode<RerollSelectedButton>("RerollSelectedButton");
        PlayerScore = GetNode<PlayerScore>("PlayerScore");
        RerollSelected.SetLabel(PlayerRerollRemaining);
    }


    public void OnRerollSelectedPressed()
    {
        if (PlayerRerollRemaining > 0)
        {
            SetRemainingRerolls(PlayerRerollRemaining - 1);
            PlayerHand.RerollSelectedDices();
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
        SetRemainingRerolls(playerRerolls);
        PlayerHand.RerollAllDices();
        score = 0;
    }


    public void PlayDices()
    {
        var dices = PlayerHand.GetDices();

        PlayerHand.RerollAllDices();
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
