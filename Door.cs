using Godot;
using System;

public partial class Door : Node2D
{
	private bool isOpen = false;
	public CollisionShape2D Collision;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		Collision = GetNode<StaticBody2D>("GateCollision").GetNode<CollisionShape2D>("CollisionShape2D");
	}
	public void Toggle()
	{
		isOpen = !isOpen;
		var Sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		if(isOpen){
			Sprite.Play("MoveOpen");
		}else{
			
			Collision.CallDeferred("set", "disabled", isOpen);
			Sprite.PlayBackwards("MoveOpen");
		}

	}
	public void OnAnimationFinished(){
		Collision.CallDeferred("set", "disabled", isOpen);
	}

}
