using Godot;
using System;

public partial class Border : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var physicsMaterial = new PhysicsMaterial
		{
			Bounce = 1,
			Friction = 0
		};

		PhysicsMaterialOverride = physicsMaterial;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
