using Godot;
using System;

public partial class Door : Area2D
{
	private bool isOpen = false;

	// Called when the node enters the scene tree for the first time.
	public void Toggle()
	{
		isOpen = !isOpen;

		Sprite doorSprite = GetNode<Sprite>("Sprite");
		doorSprite.Texture = isOpen ? (Texture)GD.Load("res://porta_aberta.png") : (Texture)GD.Load("res://porta_fechada.png");
	}

}
