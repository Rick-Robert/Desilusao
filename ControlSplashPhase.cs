using Godot;
using System;

public partial class ControlSplashPhase : Control
{
	[Signal]
	public delegate void FinishedAnimationEventHandler();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GlobalPosition = new Vector2(800,450);
		if(((Node2D)GetParent()).Scale.X  != 1){
			Scale = Vector2.One*(float)(1/((Node2D)GetParent()).Scale.X);
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnBodyEntered(Node2D Body){
		if(Body == null)
			GetNode<AnimationPlayer>("AnimationPlayer").Play("SplashReturn");
		else if(Body.Name == "Player")
			GetNode<AnimationPlayer>("AnimationPlayer").Play("Splash");
		GD.Print("Play");
	}
	public void OnAnimationFinished(StringName Animation_){
		EmitSignal(SignalName.FinishedAnimation);
	}
}
