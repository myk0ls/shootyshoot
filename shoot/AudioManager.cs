using Godot;
using System;

public partial class AudioManager : Node
{

	CharacterBody3D player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = GetNode<CharacterBody3D>("/root/Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
/*
using Godot;
using System;

public class AudioManager : Node
{
    private static AudioManager instance = null;

    // Property to get the instance
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                // If the instance is null, find the AudioManager in the scene
                instance = (AudioManager)GD.FindNode("AudioManager");
                if (instance == null)
                {
                    // If the AudioManager is not found, create a new one
                    instance = new AudioManager();
                    instance.Name = "AudioManager";
                    // Add the new AudioManager to the current scene
                    Node currentScene = (Node)GD.CurrentScene();
                    currentScene.AddChild(instance);
                }
            }
            return instance;
        }
    }

    // Prevent instantiation of the AudioManager
    private AudioManager() { }

    // Play a sound effect
    public void PlaySFX(string sfxName)
    {
        // Implementation of playing a sound effect
    }

    // Play background music
    public void PlayMusic(string musicName)
    {
        // Implementation of playing background music
    }
}


*/


public class NewsAgency
{
    public delegate void NewsEventHandler(string news);
    public event NewsEventHandler NewsPublished;

    public void ReleaseNews(string news)
    {
        NewsPublished?.Invoke(news);
    }
}
public class Newspaper
{
    public void Subscribe(NewsAgency agency)
    {
        agency.NewsPublished += ReceiveNews;
    }
    private void ReceiveNews(string news)
    {
        Console.WriteLine($"Newspaper received news: {news}");
    }
}