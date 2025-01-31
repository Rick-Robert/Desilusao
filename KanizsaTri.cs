using Godot;
using System;

public partial class KanizsaTri : Node2D
{
	[Export]
	public float ScaleFactor = (float) 0.5;
	[Signal]
	public delegate void CompletedEventHandler(); //Ativa quando puzzle completo
	private RigidBody2D LastBody;
	private string LastPoint;
	private int RightPlace = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
		for(int i = 0; i < GetChildCount(); i++){
			((Node2D)GetChild(i)).Position *= ScaleFactor;
			if(((Node2D)GetChild(i)) is not Area2D)
				((Node2D)GetChild(i)).Scale *= ScaleFactor;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(RightPlace == 3){
			RightPlace = 0;
			EmitSignal(SignalName.Completed);
			QueueFree();
		}
	}

	public void OnBodyTipDownEntered(RigidBody2D Body){
		if(Body.Name == "TipDown")
		{
			RightPlace++;
			Body.SetCollisionMask(Body.GetCollisionMask()-1); //Objeto deixa de ser visível
			LastBody = Body; //Guarda qual objeto entrou na área por último
			LastPoint = "TipDown"; //qual área entrou por último
		}
	}
	public void OnBodyLeftEntered(RigidBody2D Body){
		if(Body.Name == "TopLeft")
		{
			RightPlace++;
			Body.SetCollisionMask(Body.GetCollisionMask()-1);
			LastBody = Body;
			LastPoint = "Left";
		}
	}
		public void OnBodyRightEntered(RigidBody2D Body){
		if(Body.Name == "TopRight")
		{
			RightPlace++;
			Body.SetCollisionMask(Body.GetCollisionMask()-1);
			LastBody = Body;
			LastPoint = "Right";
			
		}
		
	}

	public void OnDragCanPose(){
		LastBody.LinearDamp = 20;
		LastBody.LinearVelocity = Vector2.Zero;
		GD.Print(LastBody.Name, " Position: ", LastBody.Position);
		LastBody.Position = LastBody.GetParent().GetNode<Node2D>("Kanizsa_Tri").Position + GetNode<Area2D>(LastPoint).Position;
		GD.Print(LastBody.Name, " After Position: ", LastBody.Position);
		var Temp = GetNode<Area2D>(LastPoint);
		if(Temp.GetCollisionMask() != 0)
		{
			Temp.SetCollisionMask(0);
		}
	}
}
