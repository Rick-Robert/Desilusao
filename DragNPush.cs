using Godot;
using System;

public partial class DragNPush : RigidBody2D
{
	[Signal]
	public delegate void CanPoseEventHandler();
	public Vector2 Dist = Vector2.Zero;
	private Node2D Player;
	private bool Draggable = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(GetCollisionMask()%2 == 0)
		{
			if(Draggable){
				EmitSignal(SignalName.CanPose);
				GD.Print("CanPose");
				Draggable = false;
			}
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
		Draggable = true;
		Player = Body;
		Dist = Position - Body.Position;
		//GD.Print("Entered" + Player.Name);
	}
	public void OnBodyExited(Node2D Body){
		Draggable = false;
		//GD.Print("Exited");
	}
	public void OnKanizsaTriCompleted(){
		QueueFree();
	}
}

