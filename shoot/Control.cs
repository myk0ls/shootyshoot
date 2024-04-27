using Godot;
using System;

public partial class Control : Godot.Control
{
	Player player;
	gun gun;
	Label ammoLabel;
	Label healthLabel;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        player = (Player)GetNode<CharacterBody3D>("/root/World/Player");
        ammoLabel = GetNode<Label>("AmmoLabel");
        healthLabel = GetNode<Label>("HealthLabel");
        gun = (gun)GetNode<Node>("Gun");
		ammoLabel.Text = "AmmoClip:" + gun.ammoClip + "\n" +
						 "AmmoTotal:" + gun.ammoTotal;
		player.GunShot += UpdateAmmo;
		gun.GunReload += UpdateAmmo;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateAmmo()
	{
        ammoLabel.Text = "AmmoClip:" + gun.ammoClip + "\n" +
                         "AmmoTotal:" + gun.ammoTotal;
    }

	public void UpdateHealth()
	{
        healthLabel.Text = "HP:" + player.health;
    }
}
