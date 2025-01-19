using Godot;
using System;

public partial class DragNPush : RigidBody2D
{
	[Export]
	public String SetAnimation = "IdleTip";
	[Export]
	public float Scaling = 1;
	[Signal]
	public delegate void CanPoseEventHandler();
	public Vector2 Dist = Vector2.Zero, InitialPosition;
	private Player Player; private bool Draggable; private String DragObject = null; 
	private CollisionShape2D Collision; private AnimatedSprite2D AnimatedSprite;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Draggable = false;
		Collision = GetNode<CollisionShape2D>("CollisionShape2D");
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
		if(!Draggable) LinearDamp = 20;
		if(Draggable) {
			//GD.Print("Player " + Player.Velocity);
			//GD.Print("Object " + LinearVelocity+" -\n");
		}
		if(GetCollisionMask()%2 == 0)
		{
			if(Draggable){
				Draggable = false;
				GD.Print("CanPose");
				LinearVelocity = Vector2.Zero;
				EmitSignal(SignalName.CanPose);
			}
		}
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
	public void OnAnimationFinished(){
		GD.Print("Finished");
		if(Name != "Triangulo")
			QueueFree();
	}
	public void OnKanizsaTriCompleted(){
		SetCollisionLayer(0);
		GetNode<Area2D>("Area2D").SetCollisionLayer(0);
		GetNode<Area2D>("Area2D").SetCollisionMask(0);
		GD.Print("0 collision");
		String temp = SetAnimation.Remove(0,4);
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(temp + "FadeOut");
		//GD.Print(temp + "FadeOut");
	}
	
}

