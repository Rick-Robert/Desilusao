using Godot;
using System;

public partial class Triangle : RigidBody2D
{
	[Export]
	public String SetAnimation = "IdleTriangulo";
	[Export]
	public float Scaling = 1;
	[Signal]
	public delegate void CanPoseEventHandler();
	public Vector2 Dist = Vector2.Zero, InitialPosition;
	private Player Player; private bool Draggable; private String DragObject = null; 
	private CollisionPolygon2D Collision; private AnimatedSprite2D AnimatedSprite;
	private static UtilityBox Tools = new UtilityBox();
	public String Prev = " ";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AnimatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		AnimatedSprite.Modulate = new Color(Colors.Black, (float) 0);
		AnimatedSprite.SelfModulate = new Color(Colors.Black, (float) 0);
		Rotation = (float)Math.PI;
		GD.Print("Tri Ready");
		GetNode<AnimationPlayer>("AnimationPlayer").Play("FadeIn");
		//GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = SetAnimation;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		Rect2 ViewportRect = GetViewport().GetVisibleRect();
		if(!ViewportRect.HasPoint(GlobalPosition)){
			Draggable = false;
			Position = InitialPosition;
		}
		if(Player != null){
			if(Input.IsActionPressed("Drag") && Draggable && Player.Holding == 0){
				Player.DragObject = Name; 
				Player.Holding = 1;
			}
			if(Player.DragObject == Name && Draggable){
				LinearDamp = 0;
				LinearVelocity = Player.Velocity;
			}
			if(Input.IsActionJustReleased("Drag")){
				if(Player != null)
					Player.Holding = 0;
				Player.DragObject = null;
				LinearDamp = 20;
			}
		}
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
		if(Player != null)
			Player.Holding = 0;
		if(Player.DragObject == Name)
			Player.DragObject = null;
		LinearDamp = 20;
		
	}
}