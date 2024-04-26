using Godot;
using System;
using System.Security.Cryptography;

public partial class Door : CsgBox3D
{
	[Export]
	public int doorID { get; set; } = 0;

	Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        player = (Player)GetNode<CharacterBody3D>("/root/World/Player");
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void interact()
	{
		for (int i=0; i < player.keyArray.Length; i++)
		{
			if (player.keyArray[i] != null)
			{
                if (player.keyArray[i].keyID == doorID)
                {
                    GD.Print("TINKA RAKTAS");
                    QueueFree();
                    return;
                }
            }
        }
		GD.Print("UZRAKINTA");
	}
}
