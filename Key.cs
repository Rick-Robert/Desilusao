using Godot;
using System;

public partial class Key : Area2D
{
	[Export] private NodePath doorPath; 
	private Door door; 

	public override void _Ready()
	{
		
		door = GetNode<Door>(doorPath);
	}
	
	private void _on_body_entered(Node body)
	{
		if (body is Player) // Verifica se o corpo que colidiu é o jogador
		{
			
			door.Toggle();
			
			QueueFree();
		}
	}
}
