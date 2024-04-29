using Godot;
using System;

public partial class Hit : State
{
    Enemy enemy;
    Player player;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public override void Enter()
    {
        enemy = machine.GetParent<Enemy>();
        player = GetNode<Player>("/root/World/Player");
        enemy.animatedSprite.AnimationFinished += () => machine.TransitionTo("Attack");
    }

    public override void Update()
    {
        Vector3 velocity = enemy.Velocity;

        var dir = player.GlobalPosition - enemy.GlobalPosition;
        velocity.X = -dir.X * (float)10f;
        velocity.Z = -dir.Z * (float)10f;
        enemy.animatedSprite.Play("hit");
    }

    public override void Exit()
    {
        enemy.animatedSprite.AnimationFinished -= () => machine.TransitionTo("Attack");
    }
}
