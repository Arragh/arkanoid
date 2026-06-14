using Godot;
using System;

public partial class Ball : RigidBody2D
{
	[Export]
	public int Speed { get; set; } = 400;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GravityScale = 0;
		LinearVelocity = new Vector2(Speed, -Speed);
		LockRotation = false;

		// Отключить затухание скорости
		LinearDamp = 0;
		LinearDampMode = DampMode.Replace;
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		if (state.LinearVelocity.LengthSquared() > 0.1f)
		{
			state.LinearVelocity = state.LinearVelocity.Normalized() * Speed;
		}

		base._IntegrateForces(state);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetStartPosition(Vector2 startPosition)
	{
		GlobalPosition = startPosition;
	}
}
