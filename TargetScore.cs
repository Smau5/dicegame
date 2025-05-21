using Godot;
using System;

public partial class TargetScore : Label
{
    private int _targetScore = 0;
    private string _text = "";
    public override void _Ready()
    {
        _text = Text;
        SetScore(_targetScore);
    }

    public void SetScore(int score)
    {
        _targetScore = score;
        Text = $"{_text} {_targetScore}";
    }
}
