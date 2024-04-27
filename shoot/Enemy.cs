using Godot;
using System;
using System.Security;

public partial class Enemy : CharacterBody3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int health = 200;
	public float moveSpeed = 3f;
	public float attackRange = 2f;
	public int attackDamage = 25;
    bool dead = false;

    States currentState = States.Idle;

	Player player;
	Area3D hitZone;
	AnimatedSprite3D animatedSprite;
	Timer attackTimer;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
	{
		player = (Player)GetParent().GetNode<CharacterBody3D>("Player");
        hitZone = GetNode<Area3D>("Area3D");
		animatedSprite = GetNode<AnimatedSprite3D>("AnimatedSprite3D");
		attackTimer = GetNode<Timer>("AttackTimer");
		attackTimer.Timeout += () => doDamage();
		hitZone.Monitorable = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;
        var dir = player.GlobalPosition - this.GlobalPosition;

        if (dead) return;
		//if (player != null) { return; }

		if (!IsOnFloor())
		{
            velocity.Y -= gravity * (float)delta;
        }


		switch(currentState)
		{
			case States.Idle:
				animatedSprite.Play("idle");
				break;

			case States.Attack:
                animatedSprite.Play("walk");
                //var dir = player.GlobalPosition - this.GlobalPosition;
                dir.Y = 0;
                dir = dir.Normalized();

                velocity.X = dir.X * (float)moveSpeed;
				velocity.Z = dir.Z * (float)moveSpeed;
				break;

			case States.Hit:
                velocity.X = -dir.X * (float)10f;
                velocity.Z = -dir.Z * (float)10f;
                animatedSprite.Play("hit");
				currentState = States.Attack;
				break;

        }

        Velocity = velocity;

		MoveAndSlide();
	}

	public void damage(int damage)
	{
		currentState = States.Hit;
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
			doDamage();
            attackTimer.Start();
        }
	}

	public void OnExit(Node node)
	{
		if (node is Player)
		{
			attackTimer.Stop();
			GD.Print("EXIT");
		}
	}

	public void OnEnterDetection(Node node)
	{
        if (node is Player)
        {
            currentState = States.Attack;
        }
	}

	public void doDamage()
	{
		player.getDamage(attackDamage);
	}

	enum States
	{
		Idle,
		Attack,
		Hit
	}

}
