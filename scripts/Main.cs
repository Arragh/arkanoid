using Godot;
using System;

public partial class Main : Node
{
	[Export]
	public Player Player { get; set; }

	[Export]
	public Marker2D PlayerStartPosition { get; set; }

	[Export]
	public Ball Ball { get; set; }

	[Export]
	public Marker2D BallStartPosition { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Player.SetStartPosition(PlayerStartPosition.GlobalPosition);
		Ball.SetStartPosition(BallStartPosition.GlobalPosition);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
