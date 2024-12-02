using Godot;
using System;

public partial class Lever : Area2D
{
	
	[Export] public NodePath DoorPath;

	private bool isOn = false;

	
	public override void  _InputEvent(Viewport viewport, InputEvent @event, int shapeIdx)
	{
		if (@event is InputEventMouseButton eventMouse && eventMouse.Pressed)
		{
			Toggle();
		}
	}

	private void Toggle()
	{
		isOn = !isOn;
		
		// Troca a textura da alavanca de acordo com o estado
		Sprite leverSprite = GetNode<Sprite>("Sprite");
		leverSprite.Texture = isOn ? (Texture)GD.Load("res://alavanca_on.png") : (Texture)GD.Load("res://alavanca_off.png");

		// Envia o sinal para a porta
		Node door = GetNode(DoorPath);
		if (door != null)
		{
			((Door)door).Toggle();
		}
	}
}
