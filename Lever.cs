using Godot;
using System;

public partial class Lever : Area2D
{
	[Export] public NodePath ActivatePath;

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
		if (Input.IsActionJustPressed("Drag") && Pushable)
		{
			Toggle();
		}
	}

	private void Toggle()
	{
		isOn = !isOn;

		Sprite2D leverSprite = GetNode<Sprite2D>("Sprite2D");
		leverSprite.FlipH = isOn;

		// sinal para a ponte
		Node Activatable = GetNode(ActivatePath);
		if (Activatable != null && Activatable is Bridge)
		{
			((Bridge)Activatable).Activate();
		}
		if(Activatable != null && Activatable is Door){
			((Door)Activatable).Toggle();
		}
	}
}
