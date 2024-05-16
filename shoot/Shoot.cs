using Godot;
using System;

public partial class Shoot : State
{
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
        enemy.animatedSprite.AnimationFinished += ShootPlayer;

    }

    public override void Exit()
    {
        if (enemy != null)
        {
            enemy.animatedSprite.AnimationFinished -= ContinueToAttack;
            enemy.animatedSprite.AnimationFinished -= ShootPlayer;
        }
    }

    public override void Update()
    {
        Vector3 velocity = enemy.Velocity;
        //var dir = player.GlobalPosition - enemy.GlobalPosition;

        velocity.X = 0;
        velocity.Z = 0;

        enemy.Velocity = velocity;

        Vector3 predictedPlayerPos = player.GlobalPosition;

        float speed = player.Velocity.Z;
        float inaccuracyFactor = 0.15f;
        predictedPlayerPos.Z += speed * inaccuracyFactor;   

        enemy.rayCast.LookAt(predictedPlayerPos);
        enemy.animatedSprite.Play("shoot");
    }

    public void ContinueToAttack()
    {
        machine.TransitionTo("Attack");
        GD.Print("Shot your ass");
    }

    public void ShootPlayer()
    {
        if (enemy.rayCast.IsColliding() && enemy.rayCast.GetCollider() is Player)
        {
            GD.Print(enemy.rayCast.GetCollider().ToString());
            GD.Print("HIT!");
            enemy.rayCast.GetCollider().Call("getDamage", 10);
        }
        SoundManager.Instance.Play("EnemyShootSound");
    }
}
