using Godot;
using System;

public partial class Dice : Node2D
{

    public Vector2 SnapPosition = new Vector2(0, 0);
    public int Value = 0;
    private bool _enabled = true;
    [Export]
    public DiceStats stats;
    public bool Enabled
    {
        get { return _enabled; }
    }
    public override void _Ready()
    {
        // initial snapPosition
        SnapPosition = Position;
        ColorRect colorRect = GetNode<ColorRect>("ColorRect");
        colorRect.Color = stats.color;
    }

    public void Roll()
    {
        int diceRoll = stats.GetRandomNumber();
        Value = diceRoll;
        Label label = GetNode<Label>("Label");
        label.Text = $"{diceRoll}";
    }

    public void Reset()
    {
        Value = 0;
        Label label = GetNode<Label>("Label");
        label.Text = $"{Value}";

    }

    // public void SetEnabled(bool value)
    // {
    //     _enabled = value;
    //     var colorRect = GetNode<ColorRect>("ColorRect");
    //     if (value)
    //     {
    //         colorRect.Color = new Color(1, 1, 1);
    //     }
    //     else
    //     {
    //         colorRect.Color = new Color(0.5f, 0.5f, 0.5f);
    //     }
    // }


}
