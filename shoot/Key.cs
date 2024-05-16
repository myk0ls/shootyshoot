using Godot;
using System;
using System.ComponentModel;

public partial class Key : RigidBody3D
{
	[Export]
	public int keyID { get; set; } = 0;

	public CustomSignals customSignals;

	Player player;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        player = (Player)GetNode<CharacterBody3D>("/root/World/Player");
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void interact()
    {
		GD.Print("Key pickup");
		player.pickKey(this);
		customSignals.EmitSignal(nameof(customSignals.UIKeyShow));
        QueueFree();
    }
}
