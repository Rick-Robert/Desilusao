using Godot;
using System;

public partial class DragNPush : RigidBody2D
{
	[Export]
	public String SetAnimation = "IdleBall";
	[Export]
	public float Scaling = 1;
	[Export]
	public bool FlipH = false;
	[Signal]
	public delegate void CanPoseEventHandler();
	public Vector2  InitialPosition;
	private Player Player = null; private bool Draggable, Posed = false, scaled = false;  
	private CollisionShape2D Collision; private AnimatedSprite2D AnimatedSprite;
	private static UtilityBox Tools = new UtilityBox();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Scaling = Scale.X;
		Draggable = false;
		Collision = GetNode<CollisionShape2D>("CollisionShape2D");
		AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	
		AnimatedSprite.Animation = SetAnimation;
		InitialPosition = Position;
		AnimatedSprite.FlipH = FlipH;
		
		//AnimatedSprite.Scale = AnimatedSprite.Scale*Scaling;
		//Collision.Scale = Collision.Scale*Scaling;
		
		//GetNode<Area2D>("Area2D").Scale = GetNode<Area2D>("Area2D").Scale*Scaling;
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(Posed) Draggable = false;
		//if(!Draggable) LinearDamp = 20;
		if(GetCollisionMask()%2 == 0)
		{
			if(!Posed){
				Posed = true;
				Draggable = false;
				EmitSignal(SignalName.CanPose);
			}
		}
		Rect2 ViewportRect = GetViewport().GetVisibleRect();
		if(!ViewportRect.HasPoint(GlobalPosition)){
			Draggable = false;
			Position = InitialPosition;
		}

		/*if(Input.IsActionJustPressed("Drag") && Draggable && Player.Holding == 0){
			LinearDamp = 0;
			LinearVelocity = Player.Velocity;
		}*/
		if(Player != null){
			if(Input.IsActionPressed("Drag") && Draggable && Player.Holding == 0){
				Player.DragObject = Name; 
				Player.Holding = 1;
			}
			if(Player.DragObject == Name && Draggable){
				LinearDamp = 0;
				if (Player.Scale.X/AnimatedSprite.Scale.X > (float)0.3){
					GD.Print("Ratio: ", Player.Scale.X/AnimatedSprite.Scale.X);
					LinearVelocity = Player.Velocity*Mathf.Min(Player.Scale.X/AnimatedSprite.Scale.X,1);
				}
			}
			if(Input.IsActionJustReleased("Drag")){
				if(Player != null)
					Player.Holding = 0;
				Player.DragObject = null;
				LinearDamp = 20;
			}
		}
		if (!scaled){Tools.ResizeTree(this, Scaling); scaled = true;}
	}
	public void OnBodyEntered(Node2D Body){
		if(Body.Name == "Player"){
			Draggable = true;
			Player = (Player)Body;
		}
		//GD.Print("Entered" + Player.Name);
	}
	public void OnBodyExited(Node2D Body){
		if(Body.Name == "Player")
			Draggable = false;
		if(Player != null){
			Player.Holding = 0;
			if(Player.DragObject == Name)
				Player.DragObject = null;
		}
		LinearDamp = 20;
		
	}
	public void OnAnimationFinished(String Name){
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
		GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeOut");
		//GD.Print(temp + "FadeOut");
	}
	
}

