using Godot;
using System;

public partial class UtilityBox : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void ResizeTree(Node2D Node, float Scaling){
		
			for(int i = 0; i < Node.GetChildCount(); i++){
				if(Node.GetChild(i) is Node2D){
					if(Node.GetChild(i) is RigidBody2D){
						ResizeTree((Node2D)Node.GetChild(i), Scaling);
					}
					else if(Node.GetChild(i).Get("Scale").VariantType.Equals(Variant.Type.Nil)){
						((Node2D)Node.GetChild(i)).Scale *= Scaling;
						GD.Print("Name: ", Node.GetChild(i).Name, "Scale: ", ((Node2D)Node.GetChild(i)).Scale);
					}
				}
			}
		return;
	}
}
