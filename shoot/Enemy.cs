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
	[Export]
	public string ItemDropType;

    public bool dead = false;

	[Signal] public delegate void EnemyTaggedEventHandler();

	public Player player;
	public Area3D hitZone;
	public AnimatedSprite3D animatedSprite;
	public Timer attackTimer;
	public Timer wanderTimer;
	public RayCast3D rayCast;
	public PackedScene pickupFactory;
	public PickupFactory factory;
	CustomSignals customSignals;

    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public override void _Ready()
	{
		player = (Player)GetParent().GetNode<CharacterBody3D>("Player");
        hitZone = GetNode<Area3D>("Area3D");
		animatedSprite = GetNode<AnimatedSprite3D>("AnimatedSprite3D");
		attackTimer = GetNode<Timer>("AttackTimer");
		wanderTimer = GetNode<Timer>("WanderTimer");
        rayCast = GetNode<RayCast3D>("RayCast3D");
		factory = GetParent().GetNode<PickupFactory>("PickupFactory");
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		//pickupFactory = ResourceLoader.Load<PackedScene>("res://pickup_factory.tscn");
		//attackTimer.Timeout += () => doDamage();
		//customSignals.EnemyDeath += DropItem;
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
		if (health <= 0) { customSignals.EmitSignal(nameof(customSignals.EnemyDeath)); }
	}
	/*
	void DropItem()
	{
		if (!dead) return;
		if (ItemDropType != "None")
		{
            PickupItem item = factory.CreateItem(ItemDropType);
            GetParent().AddChild(item);
            item.GlobalPosition = this.GlobalPosition;
            item.Visible = true;
			item.CollisionLayer = 2;
        }
		customSignals.EmitSignal(nameof(customSignals.Score));
        customSignals.EmitSignal(nameof(customSignals.UIScore));
    }
	*/
	public void doDamage()
	{
		player.getDamage(attackDamage);
	}

}
