using Godot;

public partial class Ball : RigidBody2D
{
	private int _speed { get; set; }
	private Vector2 _direction;

	[Signal]
	public delegate void StrikeEventHandler(Brick brick);

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GravityScale = 0;
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

	public void StartBall(int speed)
	{
		_speed = speed;

		// В Godot нулевой угол не как у людей и начинается с 3 часов и прибавляется по часовой
		// Поэтому делаем против часовой (отрицательный), чтобы смотрело вверх
		var angle = GD.RandRange(-120, -60);
		float radians = Mathf.DegToRad(angle);
		_direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
		LinearVelocity = _direction * _speed;
	}

	public void StopBall()
	{
		LinearVelocity = _direction * 0;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is Brick brick)
		{
			EmitSignal(SignalName.Strike, brick);
		}
	}
}
