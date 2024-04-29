using Godot;
using System;

public partial class Tagged : State
{
    // Called when the node enters the scene tree for the first time.
    Enemy enemy;
    Player player;

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
        enemy.animatedSprite.AnimationFinished += ContinueToAttack;

    }

	public override void Exit()
	{
        if (enemy != null)
        {
            enemy.animatedSprite.AnimationFinished -= ContinueToAttack;
        }
    }

    public override void Update()
    {
        Vector3 velocity = enemy.Velocity;
        var dir = player.GlobalPosition - enemy.GlobalPosition;

        velocity.X = -dir.X * (float)0.05f;
        velocity.Z = -dir.Z * (float)0.05f;

        enemy.Velocity = velocity;
        enemy.animatedSprite.Play("hit");
    }

    public void ContinueToAttack()
    {
        machine.TransitionTo("Attack");
    }
}
