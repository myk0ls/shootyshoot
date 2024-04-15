using Godot;
using System;

public partial class Enemy : CharacterBody3D
{
	// Called when the node enters the scene tree for the first time.
	public int health = 100;
	public float moveSpeed = 2f;
	public float attackRange = 2f;
	bool dead = false;

	CharacterBody3D player;
	Area3D hitZone;
	AnimatedSprite3D animatedSprite;

	public override void _Ready()
	{
		player = GetParent().GetNode<CharacterBody3D>("Player");
        hitZone = GetNode<Area3D>("Area3D");
		animatedSprite = GetNode<AnimatedSprite3D>("AnimatedSprite3D");
		hitZone.Monitorable = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		if (dead) return;
		//if (player != null) { return; }

		var dir = player.GlobalPosition - this.GlobalPosition;
		dir.Y = 0;
		dir = dir.Normalized();

		Velocity = dir * moveSpeed;

		MoveAndSlide();
	}

	public void damage(int damage)
	{
		this.health = health - damage;
		animatedSprite.Play("hit");
		GD.Print("HP:"+health);
		if (health <= 0) { kill(); }
	}

	void kill()
	{
		dead = true;
		QueueFree();
	}

	public void attack()
	{
        GD.Print("COLLISION");
    }

	public void OnEnter(Node node)
	{
		GD.Print("COLLISION");
		GD.Print(node.Name);
		if (node is Player)
		{
			node.Call("getDamage", 25);
		}
	}
}
