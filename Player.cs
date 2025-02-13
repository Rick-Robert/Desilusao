using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public int MaxSpeed = 500;
	public int ReducedSpeed = 350;
	[Export]
	public int Acc = 2200;
	[Export]
	public float FrictionRate = 1200;
	
	private float Friction = 1200;
	public int Holding = 0;
	public String DragObject = null;
	public override void _PhysicsProcess(double delta)
	{
		Vector2 TempVelocity = Vector2.Zero;
		var dir = Input.GetVector("Left","Right","Up", "Down");
		if(Holding != 0) MaxSpeed = ReducedSpeed; else MaxSpeed = 500;
		for (int i = 0; i < GetSlideCollisionCount(); i++)
		{
			var collision = GetSlideCollision(i);
			if (collision.GetCollider() is RigidBody2D)
			{
				
				
				if(dir.Length()>0){ //if any movement button is pressed
					var rigidBody = (RigidBody2D)collision.GetCollider();
					if(rigidBody.GetCollisionMask() != 0){ //if object is not in CanPose mode
						float angle = Math.Min((float)Math.Abs((-collision.GetNormal()).AngleTo(dir)),(float)Math.PI/2);
						if (Scale.X/rigidBody.Scale.X > (float)0.3)
							rigidBody.LinearVelocity = -collision.GetNormal()*ReducedSpeed*(float)Math.Cos(angle)*Math.Min(Scale.X/rigidBody.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Scale.X, 1);
						Velocity = dir*ReducedSpeed;
						TempVelocity = Velocity;
					}
				}else{
					TempVelocity = Vector2.Zero;
				}
				
			}
		}
		CharacterMove(delta);
		MoveAndSlide();
		if(TempVelocity.Length() != 0 && dir.Length() != 0) Velocity = TempVelocity;
		
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
			Velocity = Vector2.Zero;
			
		}
	}
	public int MaskVal(int pos){
		if(pos < 1) return 0;
		return (int)Mathf.Pow(pos-1,2);
	}
}
