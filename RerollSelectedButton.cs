using Godot;
using System;

public partial class RerollSelectedButton : Button
{

    string InitialText = "";

    public override void _Ready()
    {
        InitialText = Text;
    }


    public void SetLabel(int counter)
    {
        Text = $"{InitialText} {counter}";
    }
}
