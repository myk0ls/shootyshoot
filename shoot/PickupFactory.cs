using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;

public partial class PickupFactory : Node
{
    private Dictionary<string, PickupItem> pickups = new Dictionary<string, PickupItem>();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        foreach (Node child in GetChildren())
        {
            pickups.Add(child.Name, (PickupItem)child);
        }
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public PickupItem CreatePickup(string type)
	{
        if (!pickups.ContainsKey(type))
            return null;
        return (PickupItem)pickups[type].Duplicate();
	}

    public void Print()
    {
        foreach (var pickups in pickups.Keys)
        {
            GD.Print(pickups);
        }
    }
}
