using Godot;
using System;

public partial class Main : Node
{
	private Brick[] _bricks = new Brick[10];
	private int _score;

	[Export]
	public Hud Hud { get; set; }

	[Export]
	public Player Player { get; set; }

	[Export]
	public Marker2D PlayerStartPosition { get; set; }

	[Export]
	public int PlayerSpeed { get; set; } = 400;

	[Export]
	public Ball Ball { get; set; }

	[Export]
	public Marker2D BallStartPosition { get; set; }

	[Export]
	public int BallSpeed { get; set; } = 400;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 10; j++)
			{
				var brickScene = ResourceLoader.Load<PackedScene>("res://scenes/brick.tscn");
				var brick = brickScene.Instantiate<Brick>();

				brick.GlobalPosition = new Vector2(i * 110 + 88, j * 60 + 88);

				AddChild(brick);
			}
		}

		Player.SetStartPosition(PlayerStartPosition.GlobalPosition);
		Player.SetSpeed(PlayerSpeed);

		// Ball.SetSpeed(BallSpeed);
		Ball.SetStartPosition(BallStartPosition.GlobalPosition);

		Hud.StartGame += NewGame;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void NewGame()
	{
		_score = 0;
		Hud.UpdateScore(_score);

		// Ball.SetStartPosition(BallStartPosition.GlobalPosition);
		Ball.SetSpeed(BallSpeed);
	}
}
