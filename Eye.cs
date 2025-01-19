using Godot;
using System;

public partial class Eye : Node2D
{
	public Sprite2D Inside, Iris;
	public Texture2D InsideTxtr, IrisTxtr;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Inside = GetNode<Sprite2D>("Inside");
		Iris = Inside.GetNode<Sprite2D>("Iris");
		InsideTxtr = Inside.GetTexture();
		IrisTxtr = Iris.GetTexture();
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var direction = Input.GetVector("Left","Right","Up", "Down");
		if (direction.Length() != 0)
		{
			float DirAngle = direction.Angle();
			var a = InsideTxtr.GetWidth()/3;
			var b = InsideTxtr.GetHeight()/3;
			float scale;
				if((direction.X < -0.1 || direction.X > 0.1) && (direction.Y < -0.1 || direction.Y > 0.1)){ //when it is in diagonal
					scale = (float)0.8; //reduces the distance to the center in 20%
				}else{
					scale = 1;
				}
			//distance from the center of the ellipse of axes a and b based on the angle of direction
			Iris.Position = Iris.Position.Lerp(direction.Normalized()*Mathf.Sqrt(Mathf.Abs(Mathf.Pow(a*Mathf.Cos(DirAngle),2))+Mathf.Abs(Mathf.Pow(b*Mathf.Sin(DirAngle),2)))*scale,(float)0.1);
		}else{
			//returns to center
			Iris.Position -= Iris.Position*(float)0.1;
		}
	}
}
