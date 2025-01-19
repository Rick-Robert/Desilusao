using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int MaxSpeed = 400;
	public int ReducedSpeed = 300;
	[Export]
	public int Acc = 2200;
	[Export]
	public float FrictionRate = 1200;
	
	private float Friction = 1200;
	public int Holding = 0;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 TempVelocity = Vector2.Zero;
		if(Holding != 0) MaxSpeed = ReducedSpeed; else MaxSpeed = 400;
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			if (collision.GetCollider() is RigidBody2D)
			{
				
				var dir = Input.GetVector("Left","Right","Up", "Down");
				if(dir.Length()>0){ //if any movement button is pressed
					var rigidBody = (RigidBody2D)collision.GetCollider();
					if(rigidBody.GetCollisionMask() != 0){ //if object is not in CanPose mode
						float angle = Math.Min((float)Math.Abs((-collision.GetNormal()).AngleTo(dir)),(float)Math.PI/2);
						rigidBody.LinearVelocity = -collision.GetNormal()*ReducedSpeed*(float)Math.Cos(angle);
						Velocity = dir*ReducedSpeed;
						TempVelocity = Velocity;
					}
				}
				
			}
		}
		CharacterMove(delta);
		MoveAndSlide();
		if(TempVelocity.Length() != 0) Velocity = TempVelocity;
		
	}

	public void CharacterMove(double delta){
		var direction = Input.GetVector("Left","Right","Up", "Down");
		if(direction.Length() > 0)
		{
			Friction = 1000;
			Velocity += direction*Acc*(float)delta;
			Velocity = Velocity.LimitLength(MaxSpeed);
		}else{
			
			if(Velocity.Length() >= ((Velocity/Velocity.Length())*Friction*(float)delta).Length())
			{
				Velocity -= (Velocity/Velocity.Length())*Friction*(float)delta;
				Friction += FrictionRate*(float)delta;
			}
			else
			{
				Velocity = Vector2.Zero;
			}
			
		}
	}
	public int MaskVal(int pos){
		if(pos < 1) return 0;
		return (int)Mathf.Pow(pos-1,2);
	}
}
