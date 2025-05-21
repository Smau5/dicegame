using Godot;
using System;

public partial class PlayerScore : Label
{

    private int _score = 0;
    private string _text = "";
    public override void _Ready()
    {
        _text = Text;
        SetScore(_score);
    }

    public void SetScore(int score)
    {
        _score = score;
        Text = $"{_text} {_score}";
    }

}
