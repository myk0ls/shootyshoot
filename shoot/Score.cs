using Godot;
using System;

public partial class Score : Node
{
	public int ScoreAmount = 0;

	CustomSignals customSignals;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        customSignals = GetNode<CustomSignals>("/root/CustomSignals");
		customSignals.Score += AddScore;

        //customSignals.Score += (int add) => AddScore(10);
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void AddScore()
	{
		ScoreAmount = ScoreAmount + 10;
	}
}
