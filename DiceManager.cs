using Godot;
using System;

public partial class DiceManager : Node2D
{
    public Dice DiceBeingDragged = null;
    private PlayerHand PlayerHand = null;

    public override void _Ready()
    {
        PlayerHand = GetNode<PlayerHand>("../PlayerHand");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Left)
            {
                if (mouseEvent.Pressed)
                {
                    var dice = DiceUnderCursor();
                    if (dice is not null && dice is Dice)
                    {
                        DiceBeingDragged = (Dice)dice;
                    }
                }
                else
                {
                    if (DiceBeingDragged is not null)
                    {
                        var action = ActionUnderCursor();
                        if (action is not null && action is Action)
                        {
                            (action as Action).Execute(DiceBeingDragged);
                            PlayerHand.SetDiceAsUsed(DiceBeingDragged);
                        }
                        DiceBeingDragged.Position = DiceBeingDragged.SnapPosition;
                        DiceBeingDragged = null;
                    }
                }
            }
        }
    }

    private Node ActionUnderCursor()
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var parameters = new PhysicsPointQueryParameters2D();
        parameters.Position = GetGlobalMousePosition();
        parameters.CollideWithAreas = true;
        parameters.CollisionMask = 2;
        var result = spaceState.IntersectPoint(parameters);
        if (result.Count > 0)
        {
            var highestIndex = 0;
            Node highestZNode = null;
            foreach (var node in result)
            {
                var node2D = (Node2D)node["collider"];
                Node2D parent = (Node2D)node2D.GetParent();
                if (parent.GetIndex() >= highestIndex)
                {
                    highestIndex = parent.GetIndex();
                    highestZNode = parent;
                }
            }
            return highestZNode;
        }
        return null;
    }

    private Node DiceUnderCursor()
    {
        var spaceState = GetWorld2D().DirectSpaceState;
        var parameters = new PhysicsPointQueryParameters2D();
        parameters.Position = GetGlobalMousePosition();
        parameters.CollideWithAreas = true;
        parameters.CollisionMask = 1;
        var result = spaceState.IntersectPoint(parameters);
        if (result.Count > 0)
        {
            var highestIndex = 0;
            Node highestZNode = null;
            foreach (var node in result)
            {
                var node2D = (Node2D)node["collider"];
                Node2D parent = (Node2D)node2D.GetParent();
                if (parent.GetIndex() >= highestIndex)
                {
                    highestIndex = parent.GetIndex();
                    highestZNode = parent;
                }
            }
            var dice = (Dice)highestZNode;
            if (dice.Enabled)
            {
                return highestZNode;
            }
        }
        return null;
    }


    public override void _Process(double delta)
    {
        if (DiceBeingDragged is not null)
        {
            var mousePosition = GetGlobalMousePosition();
            DiceBeingDragged.Position = mousePosition;
        }
    }

}
