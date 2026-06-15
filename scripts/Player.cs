using Godot;
using System;

public partial class Player : StaticBody2D
{
	private int _speed { get; set; } = 1;

	[Export]
	public int Xmin { get; set; } = 225;

	[Export]
	public int Xmax { get; set; } = 1695;

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("move_left", "move_right");
		Position += new Vector2(direction * _speed * (float)delta, 0);
		Position = new Vector2(Mathf.Clamp(Position.X, Xmin, Xmax), Position.Y);
	}

	public void SetStartPosition(Vector2 startPosition)
	{
		GlobalPosition = startPosition;
	}

	public void SetSpeed(int speed)
	{
		_speed = speed;
	}
}
