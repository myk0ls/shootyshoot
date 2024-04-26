using Godot;
using System;

public partial class gun : Node
{
	public int ammoTotal { get; set; } = 100;
	public int ammoClip { get; set; } = 6;
	public bool canReload = true;

	[Signal]
	public delegate void GunReloadEventHandler();

	Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetNode<CharacterBody3D>("/root/World/Player");
		GD.Print(player.ToString());
		player.GunShot += UseAmmo;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("reload"))
		{
			if (!canReload) return;

			if (ammoClip < 6)
			{
                ammoClip++;
                ammoTotal--;
				EmitSignal(nameof(GunReload));
            }
		}

	}

	public void UseAmmo()
	{
		if (ammoClip > 0)
		{
            ammoClip--;

        }
	}
}
