using Godot;
using System;

public partial class Lever : Area2D
{
	
	[Export] public NodePath DoorPath;
<<<<<<< HEAD
	private bool isOn = false;
	private bool Pushable = false;
	
	public void OnBodyEntered(Node2D _Body)
	{
		Pushable = true;
		GD.Print("Lever IN");
	}
	public void OnBodyExited(Node2D _Body)
	{
		Pushable = false;
		GD.Print("Lever OUT");
	}
	public override void _Process(double Delta)
	{
		if(Input.IsActionJustPressed("Drag") && Pushable)
		{
			Toggle();
		}

	}
=======

	private bool isOn = false;

	
	public override void  _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse && eventMouse.Pressed)
		{
			Toggle();
		}
	}

>>>>>>> 2ab65d44b8ccc6e0e72c908ef0d6b640982fc29b
	private void Toggle()
	{
		isOn = !isOn;
		
		// Troca a textura da alavanca de acordo com o estado
<<<<<<< HEAD
		Sprite2D leverSprite = GetNode<Sprite2D>("Sprite2D");
		if(isOn)
		{
			leverSprite.FlipH = true;
		}else
		{
			leverSprite.FlipH = false;
		}
=======
		Sprite leverSprite = GetNode<Sprite>("Sprite");
		leverSprite.Texture = isOn ? (Texture)GD.Load("res://alavanca_on.png") : (Texture)GD.Load("res://alavanca_off.png");

>>>>>>> 2ab65d44b8ccc6e0e72c908ef0d6b640982fc29b
		// Envia o sinal para a porta
		Node door = GetNode(DoorPath);
		if (door != null)
		{
			((Door)door).Toggle();
		}
	}
}
