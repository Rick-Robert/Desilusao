using Godot;
using System;

public partial class Balls : Sprite2D
{
	[Export]
	int Direction = 1; //-1 counter-clockwise, 1 clockwise
	[Export]
	float AngularVel = 100; 
	float Angle, Ray;
	Sprite2D Parent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AngularVel *= Direction;
		AngularVel = AngularVel*(float)Mathf.Pi/180;
		if(GetParent().GetType().ToString() == "Godot.Sprite2D" || GetParent().GetType().ToString() == "Balls")
			Parent = (Sprite2D)GetParent();
		Angle = Position.Angle();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Parent != null){
			if(Parent.GetTexture() != null)
				Ray = Parent.GetTexture().GetWidth()*(1-Scale.X)/2;
			else
				Ray = (float)12.8;
			Angle += AngularVel*(float)delta;
			Position = new Vector2(Ray*(float)Mathf.Cos(Angle), Ray*(float)Mathf.Sin(Angle));
			//GD.Print(Position);
			//GD.Print("Here: "+AngularVel*(float)delta);
		}else{
			GD.Print("null"+GlobalPosition);
		}
	}
}
