 using Godot;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public partial class Player : CharacterBody3D
{
	[Export]
	public int health = 100;
    [Export]
    public float Speed = 9.50f;
	public const float RunSpeed = 7.5f;
	public const float JumpVelocity = 4.5f;
	public const float Sensitivity = 1.5f;
	public bool canShoot = true;
    public Key[] keyArray;

    public float blend = 0f;

	[Signal]
	public delegate void GunShotEventHandler();


	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	AnimatedSprite2D gunAnimation;
	RayCast3D rayCast;
    Label healthLabel;
	AnimationPlayer gunBob;
	AnimationTree blendTree;
	Area3D interactArea;
	gun gun;
    SpotLight3D gunFLash;
	CustomSignals customSignals;

    public override void _Ready()
    {
        base._Ready();
		Input.MouseMode = Input.MouseModeEnum.Captured;
		gunAnimation = GetNode<AnimatedSprite2D>("CanvasLayer/Control/Gun/AnimatedSprite2D");
		healthLabel = GetNode<Label>("CanvasLayer/Control/HealthLabel");
		rayCast = GetNode<RayCast3D>("RayCast3D");
        gunBob = GetNode<AnimationPlayer>("CanvasLayer/Control/Gun/AnimationPlayer");
		blendTree = GetNode<AnimationTree>("CanvasLayer/Control/Gun/AnimationTree");
		gunFLash = GetNode<SpotLight3D>("Camera3D/SpotLight3D");
		interactArea = GetNode<Area3D>("InteractArea");
		gun = (gun)GetNode<Node>("CanvasLayer/Control/Gun");
		customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		keyArray = new Key[2];

		customSignals.Heal += () => getHealth(30);
        gunAnimation.AnimationFinished += () => shootAnimationEnd();
		gun.GunReload += reload;
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

			if (SoundManager.Instance.getSFX("FootstepSound").Playing == false || !IsOnFloor() == true)
				SoundManager.Instance.Play("FootstepSound");

			// play bobbin
            blend = 0f;
            blendTree.Set("parameters/Blend2/blend_amount", 0);
        }
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);

            // smooth transition into idle
            if (blend < 1)
                blend += 0.1f;
            blendTree.Set("parameters/Blend2/blend_amount", blend);
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

		if (Input.IsActionJustPressed("esc"))
		{
            if (DisplayServer.WindowGetMode() == DisplayServer.WindowMode.Fullscreen)
                DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
            else DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
        }
    }

	public void shoot()
	{
		if (gun.ammoClip == 0) { return; }
		if (canShoot == false) { return; }
		canShoot = false;
        GD.Print("shot");
		EmitSignal(nameof(Player.GunShot));
        gunAnimation.Play("shoot");
		SoundManager.Instance.Play("ShootSound");
		gunFLash.Visible = true;
		if (rayCast.IsColliding() && rayCast.GetCollider().HasMethod("damage"))
		{
			GD.Print(rayCast.GetCollider().ToString());
			//GD.Print("HIT!");
			rayCast.GetCollider().Call("damage",50);
		}
    }

	public void reload()
	{
		gun.canReload = false;
		gunAnimation.Play("reload");
		SoundManager.Instance.Play("ReloadSound");
    }

	public void shootAnimationEnd()
	{
        gun.canReload = true;
        canShoot = true;
        gunFLash.Visible = false;
        gunAnimation.Play("idle");
	}

	public void getDamage(int damage)
	{
		this.health = health - damage;
		healthLabel.Text = "HP:" + health;
		if (health <= 0)
		{
			GetTree().ReloadCurrentScene();
			//QueueFree();
		}
	}

	public void getHealth(int heal)
	{
		this.health = health + heal;
	}

	public void pickKey(Key key)
	{
		for (int i = 0; i < keyArray.Length; i++)
		{
			if (keyArray[i] == null)
			{
				keyArray[i] = key;
                break;
			}
		}
    }
}
