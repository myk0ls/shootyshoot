using Godot;
using System;

public partial class Attack : State
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

    public override void _PhysicsProcess(double delta)
    {
    }

    public override void Enter()
    {
        enemy = machine.GetParent<Enemy>();
        player = GetNode<Player>("/root/World/Player");
        enemy.wanderTimer.Stop();
        enemy.attackTimer.Timeout += TransitionShoot;
        enemy.attackTimer.Start();
    }

    public override void Update()
    {
        enemy.animatedSprite.Play("walk");
        Vector3 velocity = enemy.Velocity;

        var dir = player.GlobalPosition - enemy.GlobalPosition;
        dir.Y = 0;
        dir = dir.Normalized();

        velocity.X = dir.X * (float)enemy.moveSpeed;
        velocity.Z = dir.Z * (float)enemy.moveSpeed;

        enemy.Velocity = velocity;

        enemy.MoveAndSlide();
    }

    public override void Exit()
    {
        if (enemy != null)
        {
            enemy.attackTimer.Timeout -= TransitionShoot;
        }
    }

    public void OnHitzoneEntered(Node node)
    {
        GD.Print("COLLISION");
        GD.Print(node.Name);
        if (node is Player)
        {
            //enemy.doDamage();
            enemy.attackTimer.Start();
        }
    }

    public void OnHitzoneExit(Node node)
    {
        if (node is Player)
        {
            //enemy.attackTimer.Stop();
           // GD.Print("EXIT");
        }
    }

    public void TransitionShoot()
    {
        if (enemy.dead == false)
            machine.TransitionTo("Shoot");
        //enemy.attackTimer.Timeout -= TransitionShoot;
    }
}
