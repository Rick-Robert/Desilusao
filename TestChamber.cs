using Godot;
using System;

public partial class TestChamber : Node2D
{
	public Node2D Triangle = (Node2D)ResourceLoader.Load<PackedScene>("res://Triangle.tscn").Instantiate();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void OnKanizsaCompleted(){
		if(Triangle.GetParent() == null){
			AddChild(Triangle);
			Triangle.Position = GetNode<Node2D>("Kanizsa_Tri").Position + new Vector2((float)0.0, (float)26.0);

		}
		else{
			GD.Print("Already has a parent");
		}
	}
}
