using Godot;
using System;

public partial class Button : Area2D
{
	[Export] public NodePath ActivatePath;
	public int ObjOnArea = 0;

	public override void _Ready()
	{
		
		Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		Connect("body_exited", new Callable(this, nameof(OnBodyExited)));
	}

	private void OnBodyEntered(Node2D body)
	{
		ObjOnArea++;
		if(ObjOnArea == 1)
			Activate();
	}

	private void OnBodyExited(Node2D body)
	{
		ObjOnArea--;
		if(ObjOnArea <= 0)
			Deactivate();
	}

	private void Activate()
	{
		// alterar aparência do botão
		Sprite2D buttonSprite = GetNode<Sprite2D>("Sprite2D");
		GD.Print(((AnimatedTexture)buttonSprite.Texture).GetCurrentFrame());
		((AnimatedTexture)buttonSprite.Texture).SetCurrentFrame(1);
		// ativar porta
		Node Activatable = GetNode(ActivatePath);
		if (Activatable != null && Activatable is Bridge)
		{
			((Bridge)Activatable).Activate();
		}
		if(Activatable != null && Activatable is Door){
			((Door)Activatable).Toggle();
		}
	}

	private void Deactivate()
	{
		// restaurar aparencia do botão
		Sprite2D buttonSprite = GetNode<Sprite2D>("Sprite2D");
		GD.Print(((AnimatedTexture)buttonSprite.Texture).GetCurrentFrame());
		((AnimatedTexture)buttonSprite.Texture).SetCurrentFrame(0);
		// desativar porta
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
