using Godot;
using System;

public partial class Hud : CanvasLayer
{
	[Export]
	public Button StartButton { get; set; }

	[Export]
	public Label ScoreLabel { get; set; }

	[Signal]
	public delegate void StartGameEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		StartButton.Pressed += OnStartButtonPressed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateScore(int score)
	{
		ScoreLabel.Text = score.ToString();
	}

	private void OnStartButtonPressed()
	{
		StartButton.Hide();
		EmitSignal(SignalName.StartGame);
	}
}
