using Godot;
using System;

public partial class Ball : RigidBody2D
{
	private int _speed { get; set; } = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GravityScale = 0;
		LinearVelocity = new Vector2(_speed, -_speed);
		LockRotation = false;

		// Отключить затухание скорости
		LinearDamp = 0;
		LinearDampMode = DampMode.Replace;

		ContactMonitor = true; // Отслеживать контакты мяча, иначе BodyEntered не сработает (встроенное ограничение RigidBody2D)
		MaxContactsReported = 1; // Максимум отслеживаемых контактов за кадр
		BodyEntered += OnBodyEntered;
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		if (state.LinearVelocity.LengthSquared() > 0.1f)
		{
			state.LinearVelocity = state.LinearVelocity.Normalized() * _speed;
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

	public void SetSpeed(int speed)
	{
		_speed = speed;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Brick brick)
		{
			brick.Dissapear();
		}
	}
}
