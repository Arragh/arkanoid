using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Main : Node
{
	private List<Brick> bricks = new();
	private int _score;
	private PackedScene _brickScene;

	[Export]
	public Hud Hud { get; set; }

	[Export]
	public DeadZone DeadZone { get; set; }

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
		_brickScene = ResourceLoader.Load<PackedScene>("res://scenes/brick.tscn");

		Hud.StartGame += NewGame;
		Ball.Strike += Strike;
		DeadZone.GameOver += GameOver;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
	}

	private void NewGame()
	{
		AddBricks();
		ResetLabels();
		ResetPlayerPosition();
		ResetBallPosition();
	}

	private void Strike(Brick brick)
	{
		_score++;
		Hud.UpdateScore(_score);

		brick.QueueFree();
		bricks.Remove(brick);
		
		if (bricks.Count == 0)
		{
			Win();
		}
	}

	private async void GameOver()
	{
		Hud.GameOverLabel.Show();
		await Task.Delay(TimeSpan.FromSeconds(2));
		Hud.StartButton.Show();
		Ball.StopBall();
	}

	private async void Win()
	{
		Hud.WinLabel.Show();
		Ball.StopBall();
		await Task.Delay(TimeSpan.FromSeconds(2));
		Hud.StartButton.Show();
	}

	private void AddBricks()
	{
		foreach (var brick in bricks)
		{
			brick.QueueFree();
		}

		bricks.Clear();

		for (int i = 0; i < 16; i++)
		{
			for (int j = 0; j < 5; j++)
			{
				var brick = _brickScene.Instantiate<Brick>();
				brick.GlobalPosition = new Vector2(i * 110 + 88, j * 60 + 88);

				bricks.Add(brick);

				AddChild(brick);
			}
		}
	}

	private void ResetPlayerPosition()
	{
		Player.SetStartPosition(PlayerStartPosition.GlobalPosition);
		Player.SetSpeed(PlayerSpeed);
	}

	private void ResetBallPosition()
	{
		Ball.Freeze = false; 
		Ball.LinearVelocity = Vector2.Zero;
		Ball.AngularVelocity = 0;

		Ball.SetStartPosition(BallStartPosition.GlobalPosition);
		Ball.StartBall(BallSpeed);
	}

	private void ResetLabels()
	{
		_score = 0;
		Hud.UpdateScore(_score);
		Hud.GameOverLabel.Hide();
		Hud.WinLabel.Hide();
	}
}
