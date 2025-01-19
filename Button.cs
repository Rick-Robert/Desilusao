using Godot;
using System;

public partial class Button : Area2D
{
	[Export] public NodePath DoorPath;

	private bool isOn = false;

	public override void _Ready()
	{
		
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		Connect("body_exited", new Callable(this, nameof(OnBodyExited)));
	}

	private void OnBodyEntered(Node2D body)
	{
		if (!isOn)
		{
			Activate();
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (isOn)
		{
			Deactivate();
		}
	}

	private void Activate()
	{
		isOn = true;

		// alterar aparência do botão
		Sprite2D buttonSprite = GetNode<Sprite2D>("Sprite2D");
		buttonSprite.Modulate = new Color(0, 1, 0);

		// ativar porta
		Node door = GetNode(DoorPath);
		if (door != null)
		{
			((Door)door).Toggle();
		}
	}

	private void Deactivate()
	{
		isOn = false;

		// restaurar aparencia do botão
		Sprite2D buttonSprite = GetNode<Sprite2D>("Sprite2D");
		buttonSprite.Modulate = new Color(1, 1, 1);

		// desativar porta
		Node door = GetNode(DoorPath);
		if (door != null)
		{
			((Door)door).Toggle();
		}
	}
}
