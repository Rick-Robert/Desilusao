using Godot;
using System;

public partial class Phases : Node2D
{
    
    public bool Resized = false;
	public Node2D Triangle;
	private static UtilityBox Tools = new UtilityBox();
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
	public void OnKanizsaCompleted(){
		GD.Print("Completed");
        Triangle = (Node2D)ResourceLoader.Load<PackedScene>("res://Triangle.tscn").Instantiate();
		if(Triangle.GetParent() == null){
			AddChild(Triangle);
			Tools.ResizeTree((Node2D)Triangle, (float) GetNode<KanizsaTri>("Kanizsa_Tri").ScaleFactor);
			Triangle.Position = GetNode<Node2D>("Kanizsa_Tri").Position;
			//GD.Print("NOW: "+Triangle.GetNode<AnimationPlayer>("AnimationPlayer").CurrentAnimation);
			//Triangle.InitialPosition = Triangle.Position;
		}
		else{
			GD.Print("Already has a parent");
		}
	}
}
