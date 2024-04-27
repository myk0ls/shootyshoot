using Godot;
using System;

public partial class Pickup : RigidBody3D
{
	[Export]
	public string typeOf = "ammo";

	Player player;
	gun gun;
	Label ammoLabel;
    Label healthLabel;
	Control control;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        player = (Player)GetNode<CharacterBody3D>("/root/World/Player");
        gun = (gun)GetNode<Node>("/root/World/Player/CanvasLayer/Control/Gun");
        ammoLabel = GetNode<Label>("/root/World/Player/CanvasLayer/Control/AmmoLabel");
		healthLabel = GetNode<Label>("/root/World/Player/CanvasLayer/Control/HealthLabel");
		control = GetNode<Control>("/root/World/Player/CanvasLayer/Control");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void interact()
	{
		switch (typeOf)
		{
			case "ammo":
				gun.ammoTotal += 30;
				control.UpdateAmmo();
				break;

			case "heal":
				player.health += 30;
				control.UpdateHealth();
				break;
		}
        QueueFree();
    }
}
