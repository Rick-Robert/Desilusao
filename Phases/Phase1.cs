using Godot;
using System;

public partial class Phase1 : Node2D
{
	public bool Resized = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("Resize")){
			GD.Print(DisplayServer.WindowGetMode());
			if(DisplayServer.WindowGetMode().Equals(DisplayServer.WindowMode.Fullscreen))
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
			else
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
	}
}
