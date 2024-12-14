using Godot;
using System;

public partial class DragNPush : RigidBody2D
{
	[Export]
	public String SetAnimation = "IdleTip";
	[Export]
	public String SetAnimation = "IdleTip";
	[Signal]
	public delegate void CanPoseEventHandler();
	public Vector2 Dist = Vector2.Zero;
	private Node2D Player;
	private bool Draggable = false;
	private Vector2 InitialPosition;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = SetAnimation;
		InitialPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if(GetCollisionMask()%2 == 0)
		{
			if(Draggable){
				Draggable = false;
				GD.Print("CanPose");
				EmitSignal(SignalName.CanPose);
			}
		}
		Rect2 ViewportRect = GetViewport().GetVisibleRect();
		if(!ViewportRect.HasPoint(GlobalPosition)){
			Draggable = false;
			Position = InitialPosition;
		}
		if(Input.IsActionJustPressed("Drag") && Draggable){
			Dist = Position - Player.Position;
		}
		if(Input.IsActionPressed("Drag") && Draggable){
			Position = Player.Position + Dist;
		}
		if(LinearVelocity != Vector2.Zero){
			LinearVelocity = Vector2.Zero;
		}
	}
	public void OnBodyEntered(Node2D Body){
		if(Body.Name == "Player"){
			Draggable = true;
			Player = Body;
			Dist = Position - Body.Position;
		}
		//GD.Print("Entered" + Player.Name);
	}
	public void OnBodyExited(Node2D Body){
		if(Body.Name == "Player")
			Draggable = false;
		//GD.Print("Exited");
	}
	public void OnAnimationFinished(){
		if(Name != "Triangulo")
			QueueFree();
	}
	public void OnKanizsaTriCompleted(){
		String temp = SetAnimation.Remove(0,4);
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(temp + "FadeOut");
		GD.Print(temp + "FadeOut");
	}
	
}

