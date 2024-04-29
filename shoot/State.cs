using Godot;
using System;

public partial class State : Node
{
    // Called when the node enters the scene tree for the first time.
    public StateMachine machine;
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

    public virtual void Update()
    {

    }

    public virtual void Exit()
    {
        GD.Print("EXITAS");
    }
    public virtual void Enter()
    {
        GD.Print("ENTERIS");
    }
}
