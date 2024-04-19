using Godot;
using System;

public partial class InteractArea : Area3D
{
	Node interactObject;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("interact") && interactObject != null)
		{
			interactObject.Call("interact");
		}
	}

	public void OnEnter(Node node)
	{
        if (node.HasMethod("interact"))
		{
			GD.Print(node.ToString());
			interactObject = node;
		}
	}

	public void OnExit(Node node)
	{
		interactObject = null;
	}
}
