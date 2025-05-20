using Godot;
using System;


public partial class Battle : Node2D
{
    int PlayerThrowsRemaining = 3;
    private PlayerHand PlayerHand = null;
    private RerollSelectedButton RerollSelected = null;
    public override void _Ready()
    {
        GetViewport().PhysicsObjectPickingSort = true;
        GetViewport().PhysicsObjectPickingFirstOnly = true;


        PlayerHand = GetNode<PlayerHand>("PlayerHand");
        RerollSelected = GetNode<RerollSelectedButton>("RerollSelectedButton");
        RerollSelected.SetLabel(PlayerThrowsRemaining);
    }


    public void OnRerollSelectedPressed()
    {
        if (PlayerThrowsRemaining > 0)
        {
            PlayerThrowsRemaining--;
            PlayerHand.RerollSelectedDices();
            RerollSelected.SetLabel(PlayerThrowsRemaining);
        }
    }


    public void ResetBattle()
    {
        PlayerThrowsRemaining = 3;
        RerollSelected.SetLabel(PlayerThrowsRemaining);
        PlayerHand.RerollAllDices();
    }


    public void PlayDices()
    {
        GD.Print("play");

    }


}
