using Godot;
using System;

public partial class Player : Node2D
{
	public int Speed = 500;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Position = new Vector2(300,250);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Left"))
		{
			Position += Vector2.Left*Speed*(float)delta; // i = i + 2; ==> i += 2;
		}
		if(Input.IsActionPressed("Right"))
		{
			Position += Vector2.Right*Speed*(float)delta; // i = i + 2; ==> i += 2;
		}
		if(Input.IsActionPressed("Down"))
		{
			Position += Vector2.Down*Speed*(float)delta; // i = i + 2; ==> i += 2;
		}
		if(Input.IsActionPressed("Up"))
		{
			Position += Vector2.Up*Speed*(float)delta; // i = i + 2; ==> i += 2;
		}
	}
}
