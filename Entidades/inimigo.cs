using Godot;
using System;
using System.Collections.Generic;

public partial class inimigo : CharacterBody3D
{

	[Export]
	public Path3D Caminho { get; set; }
	
	private int current_point = 0;
	[Export]
	public int Speed { get; set; } = 5;
	
	private List<Godot.Vector3> pathPoints;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalPosition = Caminho.GlobalPosition;
		pathPoints = new List<Godot.Vector3>();

		for(int i = 0; i < Caminho.Curve.PointCount; i++) {
			pathPoints.Add(Caminho.Curve.GetPointPosition(i) + Caminho.GlobalPosition);
		}
	
	}

	private void updateNextPoint() 
	{
		current_point++;
		if (current_point >= pathPoints.Count) {
			current_point = 0;
		}
	}
	public override void _Process(double delta)
	{
		Godot.Vector3 velocity = Velocity;
		Godot.Vector3 direction = (pathPoints[current_point] - GlobalPosition).Normalized();
		Godot.Vector3 step = direction * Speed * (float)delta;

		if (Math.Floor(GlobalPosition.DistanceTo(pathPoints[current_point]))<= step.Length()) {
			updateNextPoint();
		}

		velocity.X = direction.X * Speed;
		velocity.Z = direction.Z * Speed;		

		Velocity = velocity;
		MoveAndSlide();
	}
}
