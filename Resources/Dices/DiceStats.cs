using System;
using Godot;

[GlobalClass]
public partial class DiceStats : Resource
{
    [Export]
    public int[] Numbers = new int[6];
    public int GetRandomNumber()
    {
        if (Numbers == null || Numbers.Length == 0)
            return 0;
        var random = new Random();
        int index = random.Next(0, Numbers.Length);
        return Numbers[index];
    }
}
