using Godot;
using System;

public partial class SplashScreenManager : Control
{
	[Export]
    String NextPhasePath;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<AnimationPlayer>("AnimationPlayer").Play("Fade-In");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnAnimationFinished(StringName _Animation){
		if (!string.IsNullOrEmpty(NextPhasePath))
        {
            GetTree().ChangeSceneToFile(NextPhasePath);
        }
        else
        {
            GD.PrintErr("Next scene path is invalid or empty.");
        }
	}
}
