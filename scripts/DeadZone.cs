using Godot;

public partial class DeadZone : Area2D
{
	[Signal]
	public delegate void GameOverEventHandler();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += OnBallEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBallEntered(Node node)
	{
		if (node is Ball)
		{
			EmitSignal(SignalName.GameOver);
		}
	}
}
