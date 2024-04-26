using Godot;
using System;

public partial class Control : Godot.Control
{
	Player player;
	gun gun;
	Label ammoLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        player = (Player)GetNode<CharacterBody3D>("/root/World/Player");
        ammoLabel = GetNode<Label>("AmmoLabel");
        gun = (gun)GetNode<Node>("Gun");
		ammoLabel.Text = "AmmoClip:" + gun.ammoClip + "\n" +
						 "AmmoTotal:" + gun.ammoTotal;
		player.GunShot += Update;
		gun.GunReload += Update;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Update()
	{
        ammoLabel.Text = "AmmoClip:" + gun.ammoClip + "\n" +
                         "AmmoTotal:" + gun.ammoTotal;
    }
}
