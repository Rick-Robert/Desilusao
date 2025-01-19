using Godot;
using System;

public partial class Triangle : RigidBody2D
{
	[Export]
	public String SetAnimation = "IdleTip";
	[Export]
	public float Scaling = 1;
	[Signal]
	public delegate void CanPoseEventHandler();
	public Vector2 Dist = Vector2.Zero, InitialPosition;
	private Player Player; private bool Draggable; private String DragObject = null; 
	private CollisionPolygon2D Collision; private AnimatedSprite2D AnimatedSprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Draggable = false;
		Collision = GetNode<CollisionPolygon2D>("CollisionPolygon2D");
		AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	
		AnimatedSprite.Animation = SetAnimation;
		AnimatedSprite.Scale = AnimatedSprite.Scale*Scaling;
		Collision.Scale = Collision.Scale*Scaling;
		InitialPosition = Position;
		GetNode<Area2D>("Area2D").Scale = GetNode<Area2D>("Area2D").Scale*Scaling;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Rect2 ViewportRect = GetViewport().GetVisibleRect();
		if(!ViewportRect.HasPoint(GlobalPosition)){
			Draggable = false;
			Position = InitialPosition;
		}
		if(Input.IsActionJustPressed("Drag") && Draggable && Player.Holding == 0){
			Dist = Position - Player.Position;
			LinearDamp = 0;
			LinearVelocity = Player.Velocity;
		
		}
		if(Input.IsActionPressed("Drag") && Draggable){
			LinearDamp = 0;
			if (DragObject == null && Player.Holding == 0) {DragObject = SetAnimation; Player.Holding = 1;}
			if(DragObject == SetAnimation){
				LinearVelocity = Player.Velocity;
			}
				//Position = Player.Position + Dist;
				
			
		}
		if(Input.IsActionJustReleased("Drag")){
			if(Player != null)
				Player.Holding = 0;
			DragObject = null;
			LinearDamp = 20;
		}
	}
	public void OnBodyEntered(Node2D Body){
		if(Body.Name == "Player"){
			Draggable = true;
			Player = (Player)Body;
			Dist = Position - Body.Position;
		}
		//GD.Print("Entered" + Player.Name);
	}
	public void OnBodyExited(Node2D Body){
		if(Body.Name == "Player")
			Draggable = false;
		
	}
	
}

