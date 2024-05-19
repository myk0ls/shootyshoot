using Godot;
using System;

public partial class Death : State
{
    CustomSignals customSignals;
    Enemy enemy;

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
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
        enemy = machine.GetParent<Enemy>();
        Die();
        DropItem();
        enemy.CollisionLayer = 2;
        enemy.CollisionMask = 1;
    }

    public override void Exit()
    {
        if (enemy != null)
        {
            machine.TransitionTo("Death");
        }
    }

    public override void Update()
    {
        enemy.Velocity = Vector3.Zero;
    }

    void Die()
    {
        enemy.dead = true;
        enemy.animatedSprite.Play("die");

    }

    void DropItem()
    {
        if (enemy.ItemDropType != "None")
        {
            PickupItem item = enemy.factory.CreateItem(enemy.ItemDropType);
            GetParent().GetParent().AddChild(item);
            item.GlobalPosition = enemy.GlobalPosition;
            item.Visible = true;
            item.CollisionLayer = 2;
        }
        customSignals.EmitSignal(nameof(customSignals.Score));
        customSignals.EmitSignal(nameof(customSignals.UIScore));
    }
}
