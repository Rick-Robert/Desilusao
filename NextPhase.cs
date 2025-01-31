using Godot;
using System;

public partial class NextPhase : Node2D
{
	[Export]
    String NextPhasePath;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnPortalBodyEntered(Node2D Body){
		if(Body.Name == "Player")
			CallDeferred(nameof(ChangeSceneToNext));
	}
	private void ChangeSceneToNext()
    {
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
