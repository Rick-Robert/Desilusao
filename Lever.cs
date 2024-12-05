using Godot;
using System;

public partial class Lever : Area2D
{
	//Node LeverScene = ResourceLoader.Load<PackedScene>("res://Lever.tscn").Instantiate();
	[Export] public NodePath DoorPath;

	private bool isOn = false;

	public override void _Ready(){
		//GD.Print(ResourceLoader.Load("res://Lever.tscn").GetType());
	}
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
		Sprite2D leverSprite = GetNode<Sprite2D>("/root/Lever/Sprite2D");
		leverSprite.Texture = isOn ? (Texture2D)GD.Load("res://alavanca_on.png") : (Texture2D)GD.Load("res://alavanca_off.png");

		// Envia o sinal para a porta
		Node door = GetNode(DoorPath);
		if (door != null)
		{
			((Door)door).Toggle();
		}
	}
}
