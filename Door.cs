using Godot;
using System;

public partial class Door : Area2D
{
	private bool isOpen = false;
<<<<<<< HEAD
	
=======

>>>>>>> 2ab65d44b8ccc6e0e72c908ef0d6b640982fc29b
	// Called when the node enters the scene tree for the first time.
	public void Toggle()
	{
		isOpen = !isOpen;

<<<<<<< HEAD
		Sprite2D doorSprite = GetNode<Sprite2D>("Sprite2D");
		if(isOpen)
		{
			doorSprite.FlipH = true;
		}else
		{
			doorSprite.FlipH = false;
		}
=======
		Sprite doorSprite = GetNode<Sprite>("Sprite");
		doorSprite.Texture = isOpen ? (Texture)GD.Load("res://porta_aberta.png") : (Texture)GD.Load("res://porta_fechada.png");
>>>>>>> 2ab65d44b8ccc6e0e72c908ef0d6b640982fc29b
	}

}
