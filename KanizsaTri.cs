using Godot;
using System;

public partial class KanizsaTri : Node2D
{
	[Signal]
	public delegate void CompletedEventHandler();
	private RigidBody2D LastBody;
	private string LastPoint;
	private int RightPlace = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(RightPlace == 3){
			RightPlace = 0;
			EmitSignal(SignalName.Completed);
		}
	}

	public void OnBodyTipDownEntered(RigidBody2D Body){
		RightPlace++;

		GD.Print(Body.Position);
		GD.Print(Body.GetCollisionMask()-1);
		GD.Print("TipDown IN: " + RightPlace);

		Body.SetCollisionMask(Body.GetCollisionMask()-5); //Objeto deixa de ser visível
		Body.SetCollisionLayer(Body.GetCollisionLayer()+4); //Objeto existe na camada 5
		Body.GetNode<Area2D>("Area2D").SetCollisionMask(Body.GetNode<Area2D>("Area2D").GetCollisionMask()-1); //Area 2D de Objeto para de ver o player
		
		LastBody = Body; //Guarda qual objeto entrou na área por último
		LastPoint = "TipDown"; //qual área entrou por último
	}
	public void OnBodyLeftEntered(RigidBody2D Body){
		RightPlace++;

		GD.Print(Body.GetCollisionMask());
		GD.Print(Body.Position);
		GD.Print("Left IN HERE: " + RightPlace);

		Body.SetCollisionMask(Body.GetCollisionMask()-5);
		Body.SetCollisionLayer(Body.GetCollisionLayer()+4);
		Body.GetNode<Area2D>("Area2D").SetCollisionMask(Body.GetNode<Area2D>("Area2D").GetCollisionMask()-1);

		LastBody = Body;
		LastPoint = "Left";
	}
		public void OnBodyRightEntered(RigidBody2D Body){
		RightPlace++;
		
		Body.SetCollisionMask(Body.GetCollisionMask()-5);
		Body.SetCollisionLayer(Body.GetCollisionLayer()+4);
		Body.GetNode<Area2D>("Area2D").SetCollisionMask(Body.GetNode<Area2D>("Area2D").GetCollisionMask()-1);
		
		LastBody = Body;
		LastPoint = "Right";

		GD.Print("Right IN: " + RightPlace);
		
	}

	public void OnDragCanPose(){
		LastBody.Position = LastBody.GetParent().GetNode<Node2D>("Kanizsa_Tri").Position + GetNode<Area2D>(LastPoint).Position;
		GD.Print(LastBody.Position);
		var Temp = GetNode<Area2D>(LastPoint);
		if(Temp.GetCollisionMask() != 0)
		{
			Temp.SetCollisionMask(Temp.GetCollisionMask()-8);
			Temp.SetVisibilityLayer(Temp.GetVisibilityLayer()-8);
		}
		GD.Print(Temp.GetCollisionMask());
		GD.Print(Temp.Name);
	}
}
