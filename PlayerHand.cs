using Godot;
using System;
using System.Collections.Generic;

public class HandDice
{
    public Dice Dice;
    public bool Used;
    public bool Selected = false;

    public HandDice(Dice dice)
    {
        Dice = dice;

    }
}

public partial class PlayerHand : Node2D
{
    const int DiceCount = 3;
    private PackedScene DiceScene = GD.Load<PackedScene>("res://dice.tscn");
    // Get the center X of the screen
    float PositionX = 200;
    float PositionY = 800;

    private DiceManager DiceManager = null;
    private List<HandDice> HandDices = new List<HandDice>();

    public override void _Ready()
    {
        DiceManager = GetNode<DiceManager>("../DiceManager");
        float spacing = 150;
        for (int i = 0; i < DiceCount; i++)
        {
            Dice diceInstance = DiceScene.Instantiate<Dice>();
            float x = PositionX + i * spacing;
            diceInstance.Position = new Vector2(x, PositionY);
            diceInstance.SnapPosition = new Vector2(x, PositionY);
            HandDices.Add(new HandDice(diceInstance));
            AddChild(diceInstance);
        }
    }


    public void SetDiceAsUsed(Dice usedDice)
    {
        var usedHandDice = HandDices.Find(h => h.Dice == usedDice);
        usedHandDice.Used = true;
        usedHandDice.Dice.SetEnabled(false);
    }

    public void ToggleSelected(Dice selectedDice)
    {
        var handDice = HandDices.Find(h => h.Dice == selectedDice);
        handDice.Selected = !handDice.Selected;
        if (handDice.Selected)
        {
            handDice.Dice.Scale = new Vector2(1.1f, 1.1f);
        }
        else
        {
            handDice.Dice.Scale = new Vector2(1f, 1f);
        }
    }

    public void OnRerollAllButtonPressed()
    {

        foreach (var handDice in HandDices)
        {
            handDice.Used = false;
            handDice.Dice.SetEnabled(true);
            handDice.Dice.Roll();
        }

    }

    public void OnRerollSelectedPressed()
    {
        foreach (var handDice in HandDices)
        {
            if (handDice.Selected)
            {
                ToggleSelected(handDice.Dice);
                handDice.Dice.Roll();
            }
        }
    }



}
