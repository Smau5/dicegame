using Godot;
using System;
using System.Collections.Generic;

public class HandDice
{
    public Dice Dice;
    public bool Scored;

    public HandDice(Dice dice)
    {
        Dice = dice;

    }
}

public partial class PlayerField : Node2D
{
    private PackedScene DiceScene = GD.Load<PackedScene>("res://dice.tscn");
    // Get the center X of the screen
    float PositionX = 0;
    float PositionY = 0;

    private List<HandDice> HandDices = new List<HandDice>();

    private ColorRect ColorRect = null;
    [Signal]
    public delegate void UpdateScoreEventHandler(int value);

    public override void _Ready()
    {
        ColorRect = GetNode<ColorRect>("ColorRect");
    }

    public void InitializeDices(Godot.Collections.Array<DiceStats> dices)
    {
        float spacing = 150;
        for (int i = 0; i < dices.Count; i++)
        {
            Dice diceInstance = DiceScene.Instantiate<Dice>();
            float x = spacing - (ColorRect.Size.X / 2) + i * spacing;
            diceInstance.Position = new Vector2(x, PositionY);
            diceInstance.SnapPosition = new Vector2(x, PositionY);
            diceInstance.stats = dices[i];
            HandDices.Add(new HandDice(diceInstance));
            SetDicesInitialState();
            AddChild(diceInstance);
        }
    }


    // public void SetDiceAsUsed(Dice usedDice)
    // {
    //     var usedHandDice = HandDices.Find(h => h.Dice == usedDice);
    //     usedHandDice.Used = true;
    //     usedHandDice.Dice.SetEnabled(false);
    //     SetSelected(usedHandDice.Dice, false);
    // }

    // public void ToggleSelected(Dice selectedDice)
    // {
    //     var handDice = HandDices.Find(h => h.Dice == selectedDice);
    //     SetSelected(selectedDice, !handDice.Selected);
    // }

    // public void SetSelected(Dice selectedDice, bool selected)
    // {
    //     var handDice = HandDices.Find(h => h.Dice == selectedDice);
    //     handDice.Selected = selected;
    //     if (selected)
    //     {
    //         handDice.Dice.Scale = new Vector2(1.1f, 1.1f);
    //     }
    //     else
    //     {
    //         handDice.Dice.Scale = new Vector2(1f, 1f);
    //     }
    // }

    public void SetDicesInitialState()
    {
        foreach (var handDice in HandDices)
        {
            handDice.Scored = false;
            handDice.Dice.Reset();
        }
    }


    public void RollAllDices()
    {
        foreach (var handDice in HandDices)
        {
            int value = handDice.Dice.Roll();
            SetDiceAsRolled(handDice);
            EmitSignal(SignalName.UpdateScore, value);
        }
    }

    public void SetDiceAsRolled(HandDice dice)
    {
        dice.Scored = true;
    }

    // public void SetAllDicesInitial()
    // {
    //     foreach (var handDice in HandDices)
    //     {
    //         handDice.Used = false;
    //         handDice.Dice.SetEnabled(true);
    //         handDice.Dice.Reset();
    //     }

    // }

    // public void RerollSelectedDices()
    // {
    //     foreach (var handDice in HandDices)
    //     {
    //         if (handDice.Selected)
    //         {
    //             ToggleSelected(handDice.Dice);
    //             handDice.Dice.Roll();
    //         }
    //     }
    // }


    public List<Dice> GetDices()
    {
        var dices = new List<Dice>();
        foreach (var handDice in HandDices)
        {
            dices.Add(handDice.Dice);
        }
        return dices;
    }



}
