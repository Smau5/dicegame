using Godot;
using System;

public partial class Battle : Node2D
{
    public override void _Ready()
    {
        GetViewport().PhysicsObjectPickingSort = true;
        GetViewport().PhysicsObjectPickingFirstOnly = true;
    }

}
