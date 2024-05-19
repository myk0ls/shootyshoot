using Godot;
using System;

public partial class Idle : State
{
    Enemy enemy;
	Timer idleTimer;
	Timer wanderTimer;
	CustomSignals customSignals;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _PhysicsProcess(double delta)
	{
        Vector3 velocity = enemy.Velocity;

		velocity.X = 0;
		velocity.Z = 0;

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
		enemy.EnemyTagged += EnemyTagged;
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
        idleTimer = machine.GetParent<Enemy>().GetNode<Timer>("IdleTimer");
		Random rand = new Random();
		idleTimer.WaitTime = (1 + rand.NextDouble() * 2);
        idleTimer.Start();
        idleTimer.Timeout += Wander;
		customSignals.EnemyDeath += Death;
    }

    public override void Update()
    {
        enemy.animatedSprite.Play("idle");
    }

    public override void Exit()
	{
        if (enemy != null)
        {
            idleTimer.Timeout -= Wander;
        }
    }

    public void OnPlayerDetection(Node node)
	{
        if (node is Player)
        {
            if (enemy.dead == false)
			{
                machine.TransitionTo("Attack");
                idleTimer.Stop();
            }
        }
    }

	public void EnemyTagged()
	{
		if (enemy.dead == false)
			machine.TransitionTo("Tagged");
    }

	public void Wander()
	{
        if (enemy.dead == false)
            machine.TransitionTo("Wander");
    }

	public void Death()
	{
		if (enemy.health <= 0)
		{
            machine.TransitionTo("Death");
        }
	}
}
