using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int MaxSpeed = 400;
	[Export]
	public int Acc = 2200;
	[Export]
	public float FrictionRate = 1200;
	
	private float Friction = 1200;
	
	public override void _PhysicsProcess(double delta)
	{
		var direction = Input.GetVector("Left","Right","Up", "Down");
		if(direction.Length() > 0)
		{
			Friction = 1000;
			Velocity += direction*Acc*(float)delta;
			Velocity = Velocity.LimitLength(MaxSpeed);
		}else{
			if(Velocity.Length() > ((Velocity/Velocity.Length())*Friction*(float)delta).Length())
			{
				Velocity -= (Velocity/Velocity.Length())*Friction*(float)delta;
				Friction += FrictionRate*(float)delta;
			}
			else
			{
				Velocity = Vector2.Zero;
			}
		}
		MoveAndSlide();
	}
}
