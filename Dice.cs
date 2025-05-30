using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public enum Modifier
{
    Red3,
    Red5
}

public partial class Dice : Node2D
{

    public Vector2 SnapPosition = new Vector2(0, 0);
    private int _value = 0;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            Label label = GetNode<Label>("Label");
            label.Text = $"{_value}";
        }

    }

    private bool _enabled = true;
    [Export]
    public DiceStats stats;
    public bool Enabled
    {
        get { return _enabled; }
    }

    public List<Modifier> Modifiers = new();
    private Label modifierLabel;
    public override void _Ready()
    {
        // initial snapPosition
        SnapPosition = Position;
        ColorRect colorRect = GetNode<ColorRect>("ColorRect");
        colorRect.Color = stats.color;


        modifierLabel = GetNode<Label>("ModifierLabel");
    }

    public async Task<int> Roll()
    {
        int diceRoll = stats.GetRandomNumber();
        var newValue = diceRoll;
        Value = newValue;
        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        if (Modifiers.Contains(Modifier.Red3))
        {
            newValue += 3;
            Value = newValue;
            await TriggerModifierAsync(Modifier.Red3);
        }

        if (Modifiers.Contains(Modifier.Red5))
        {
            newValue *= 2;
            Value = newValue;
            await TriggerModifierAsync(Modifier.Red5);
        }
        Value = newValue;
        return Value;
    }

    public async Task TriggerModifierAsync(Modifier modifier)
    {
        if (modifier == Modifier.Red3)
        {
            Label label = (Label)modifierLabel.Duplicate();
            label.Visible = true;
            label.Text = "+3";
            AddChild(label);
            label.Position = modifierLabel.Position;

            var tween = CreateTween();
            tween.TweenProperty(label, "position", label.Position + new Vector2(0, -40), 1.0f);
            tween.TweenCallback(Callable.From(() => label.QueueFree()));
            await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        }
        else if (modifier == Modifier.Red5)
        {
            Label label = (Label)modifierLabel.Duplicate();
            label.Visible = true;
            label.Text = "X2";
            AddChild(label);
            label.Position = modifierLabel.Position;

            var tween = CreateTween();
            tween.TweenProperty(label, "position", label.Position + new Vector2(0, -40), 1.0f);
            tween.TweenCallback(Callable.From(() => label.QueueFree()));
            await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        }
    }

    public void Reset()
    {
        Value = 0;
        Modifiers.Clear();
    }

    public void AddModifier(Modifier modifier)
    {
        Modifiers.Add(modifier);
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
