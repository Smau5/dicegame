using System;
using Godot;

public enum DiceColor
{
    Red,
    Blue,
    
}

[GlobalClass]
public partial class DiceStats : Resource
{
    [Export]
    public int[] Numbers = new int[6];
    [Export]
    public Color color = new Color(1, 1, 1, 1);
    [Export]
    public DiceColor DiceColor;
    public int GetRandomNumber()
    {
        if (Numbers == null || Numbers.Length == 0)
            return 0;
        var random = new Random();
        int index = random.Next(0, Numbers.Length);
        return Numbers[index];
    }
}
