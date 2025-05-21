using Godot;
using System;

public partial class Dice : Node2D
{

    public Vector2 SnapPosition = new Vector2(0, 0);
    public int Value = 0;
    private bool _enabled = true;
    public bool Enabled
    {
        get { return _enabled; }
    }
    public override void _Ready()
    {
        // initial snapPosition
        SnapPosition = Position;
    }

    public void Roll()
    {
        Random rnd = new Random();
        int diceRoll = rnd.Next(1, 7);
        Value = diceRoll;
        Label label = GetNode<Label>("Label");
        label.Text = $"{diceRoll}";
    }

    public void SetEnabled(bool value)
    {
        _enabled = value;
        var colorRect = GetNode<ColorRect>("ColorRect");
        if (value)
        {
            colorRect.Color = new Color(1, 1, 1);
        }
        else
        {
            colorRect.Color = new Color(0.5f, 0.5f, 0.5f);
        }
    }


}
