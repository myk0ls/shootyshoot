using Godot;
using System;

public partial class Idle : State
{
    Enemy enemy;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//enemy = GetNode<CharacterBody3D>("/root/World/Enemy");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = enemy.Velocity;

		if (!enemy.IsOnFloor())
		{
			velocity.Y -= gravity * (float)delta;
		}

		enemy.Velocity = velocity;

		enemy.MoveAndSlide();
	}

    public override void Enter()
    {
        enemy = machine.GetParent<Enemy>();
    }

	public override void Exit()
	{

	}

    public void OnPlayerDetection(Node node)
	{
        if (node is Player)
        {
			machine.TransitionTo("Attack");
        }
    }
}
