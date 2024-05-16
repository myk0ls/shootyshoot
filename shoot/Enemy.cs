using Godot;
using System;
using System.Security;

public partial class Enemy : CharacterBody3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int health = 200;
	[Export]
	public float moveSpeed = 3f;
    [Export]
    public float attackRange = 2f;
    [Export]
    public int attackDamage = 25;
    bool dead = false;

	[Signal] public delegate void EnemyTaggedEventHandler();

	public Player player;
	public Area3D hitZone;
	public AnimatedSprite3D animatedSprite;
	public Timer attackTimer;
	public Timer wanderTimer;
	public RayCast3D rayCast;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
	{
		player = (Player)GetParent().GetNode<CharacterBody3D>("Player");
        hitZone = GetNode<Area3D>("Area3D");
		animatedSprite = GetNode<AnimatedSprite3D>("AnimatedSprite3D");
		attackTimer = GetNode<Timer>("AttackTimer");
		wanderTimer = GetNode<Timer>("WanderTimer");
        rayCast = GetNode<RayCast3D>("RayCast3D");
        //attackTimer.Timeout += () => doDamage();
		hitZone.Monitorable = true;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
        if (dead) return;

	}

	public void damage(int damage)
	{
		EmitSignal(nameof(EnemyTagged));
		this.health = health - damage;
		GD.Print("HP:"+health);
		if (health <= 0) { kill(); }
	}

	void kill()
	{
		dead = true;
		QueueFree();
	}

	public void doDamage()
	{
		player.getDamage(attackDamage);
	}

}
