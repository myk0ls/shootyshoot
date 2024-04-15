using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public int health = 100;
	public const float Speed = 5.0f;
	public const float RunSpeed = 7.5f;
	public const float JumpVelocity = 4.5f;
	public const float Sensitivity = 3.5f;
	public bool canShoot = true;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	AnimatedSprite2D gunAnimation;
	RayCast3D rayCast;
	AudioStreamPlayer shotSound;
	Label healthLabel;

    public override void _Ready()
    {
        base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		gunAnimation = GetNode<AnimatedSprite2D>("CanvasLayer/Control/AnimatedSprite2D");
		healthLabel = GetNode<Label>("CanvasLayer/Control/HealthLabel");
		rayCast = GetNode<RayCast3D>("RayCast3D");
        shotSound = GetNode<AudioStreamPlayer>("ShootSound");
		gunAnimation.AnimationFinished += () => shootAnimationEnd();
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
			velocity.Y = JumpVelocity;

		if (Input.IsActionJustPressed("shoot"))
		{
			shoot();

		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("walk_left", "walk_right", "walk_foward", "walk_backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
		if (@event is InputEventMouseMotion)
		{
			InputEventMouseMotion motion = @event as InputEventMouseMotion;
			Rotation = new Vector3(Rotation.X, Rotation.Y - motion.Relative.X / 1000 * Sensitivity, Rotation.Z);
			Camera3D camera = GetNode<Camera3D>("Camera3D");
			camera.Rotation = new Vector3(Mathf.Clamp(camera.Rotation.X - motion.Relative.Y / 1000 * Sensitivity, -2, 2), camera.Rotation.Y, camera.Rotation.Z);
        }
    }

	public void shoot()
	{
		if (canShoot == false) { return; }
		canShoot = false;
        GD.Print("shot");
        gunAnimation.Play("shoot");
        shotSound.Play();
		if (rayCast.IsColliding() && rayCast.GetCollider().HasMethod("kill"))
		{
			GD.Print(rayCast.GetCollider().ToString());
			//GD.Print("HIT!");
			rayCast.GetCollider().Call("damage",50);
		}

    }

	public void shootAnimationEnd()
	{
		canShoot = true;
	}

	public void getDamage(int damage)
	{
		this.health = health - damage;
		healthLabel.Text = "HP:" + health;
		if (health <= 0)
		{
			QueueFree();
		}
	}
}
