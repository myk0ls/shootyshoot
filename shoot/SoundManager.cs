using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class SoundManager : Node
{
	public static SoundManager Instance { get; private set; }

	private Dictionary<string,AudioStreamPlayer> sfx = new Dictionary<string,AudioStreamPlayer>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = this;

		foreach (Node child in GetChildren())
		{
			sfx.Add(child.Name, (AudioStreamPlayer)child);
		}

		foreach (var sfxss in  sfx.Keys) 
		{
			GD.Print(sfxss);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Play(string name)
	{
		if (!sfx.ContainsKey(name))
			return;
		sfx[name].Play();
	}

	public AudioStreamPlayer getSFX(string name)
	{
        if (!sfx.ContainsKey(name))
            return null;
        return sfx[name];
    }
}
