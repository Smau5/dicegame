using Godot;
using System;

public partial class Action : Node2D
{

    [Export]
    public string ActionName = "placeholder";

    public override void _Ready()
    {
        var name = GetNode<Label>("Name");
        name.Text = ActionName;
    }


    public void Execute(Dice dice)
    {
        GD.Print($"{ActionName} with dice number: {dice.Value}");
    }


}
