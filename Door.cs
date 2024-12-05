using Godot;
using System;

public partial class Door : Area2D
{
	private bool isOpen = false;
	
	// Called when the node enters the scene tree for the first time.
	public void Toggle()
	{
		isOpen = !isOpen;

		//Sprite2D doorSprite = GetNode<Sprite2D>("Sprite");
		//doorSprite.Texture = isOpen ? (Texture2D)GD.Load("res://porta_aberta.png") : (Texture2D)GD.Load("res://porta_fechada.png");
	}

}
