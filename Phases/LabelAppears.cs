using Godot;
using System;

public partial class LabelAppears : Label
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Visible= false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnAreaEntered(Node2D Body){
		if(Body.Name == "Player"){
			Visible = true;
			GD.Print("Visible");
		}
	}
	public void OnAreaExited(Node2D Body){
		GD.Print("Non Visible");
		Visible = false;
		if(Body.Name == "Player"){
			GD.Print("Non Visible");
			Visible = false;
		}
	}
}
