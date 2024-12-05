using Godot;
using System;

public partial class Lever : Area2D
{
	
	[Export] public NodePath DoorPath;
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
	private void Toggle()
	{
		isOn = !isOn;
		
		// Troca a textura da alavanca de acordo com o estado
		Sprite2D leverSprite = GetNode<Sprite2D>("Sprite2D");
		if(isOn)
		{
			leverSprite.FlipH = true;
		}else
		{
			leverSprite.FlipH = false;
		}
		// Envia o sinal para a porta
		Node door = GetNode(DoorPath);
		if (door != null)
		{
			((Door)door).Toggle();
		}
	}
}
