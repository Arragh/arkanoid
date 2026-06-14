using Godot;
using System;

public partial class Player : StaticBody2D
{
	[Export]
	public int Speed { get; set; } = 400;

	public override void _Ready()
	{
		var physicsMaterial = new PhysicsMaterial
		{
			Bounce = 1,
			Friction = 0
		};

		PhysicsMaterialOverride = physicsMaterial;
		
		base._Ready();
	}

	public override void _PhysicsProcess(double delta)
	{
		float direction = Input.GetAxis("move_left", "move_right");
		Vector2 velocity = new Vector2(direction * Speed, 0);

		MoveAndCollide(velocity * (float)delta);
	}

	public void SetStartPosition(Vector2 startPosition)
	{
		GlobalPosition = startPosition;
	}
}
