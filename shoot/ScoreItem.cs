using Godot;
using System;

public partial class ScoreItem : PickupItem
{
    // Called when the node enters the scene tree for the first time.
    CustomSignals customSignals;

    public override void _Ready()
    {
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void interact()
    {
        customSignals.EmitSignal(nameof(customSignals.Score));
        customSignals.EmitSignal(nameof(customSignals.UIScore));
        QueueFree();
    }
}
