using Godot;
using System;
using System.Reflection.PortableExecutable;

public partial class Wander : State
{
    Enemy enemy;
    Timer wanderTimer;
    Random random = new Random();
    float randomX;
    float randomZ;
    public override void _Ready()
    {
    }


    public override void _Process(double delta)
    {
    }

    public override void _PhysicsProcess(double delta)
    {
    }

    public override void Enter()
    {
        enemy = machine.GetParent<Enemy>();
        wanderTimer = machine.GetParent<Enemy>().GetNode<Timer>("WanderTimer");
        wanderTimer.Start();    
        wanderTimer.Timeout += TransitionIdle;
        randomX = (float)random.NextDouble() * 2 - 1;
        randomZ = (float)random.NextDouble() * 2 - 1;
    }

    public override void Update()
    {
        enemy.animatedSprite.Play("walk");

        // Create a random direction in the XZ plane
        var dir = new Vector3();
        dir.Y = 0;
        dir.X = randomX; // Random float between -1 and 1
        dir.Z = randomZ; // Random float between -1 and 1
        dir = dir.Normalized();

        // Set the velocity in the direction
        Vector3 velocity = dir * enemy.moveSpeed; // Speed is 2 units

        enemy.Velocity = velocity;

        // Move the enemy
        enemy.MoveAndSlide();
    }

    public override void Exit()
    {
        if (enemy != null)
        {
            wanderTimer.Timeout -= TransitionIdle;
        }
    }

    public void TransitionIdle()
    {
        GD.Print("Keisk");
        machine.TransitionTo("Idle");
    }
}
