using Godot;
using System;

public partial class Bridge : StaticBody2D
{
	private int direction = -1; private bool isActive = false;
	private Sprite2D BridgeSprite;

	public override void _Ready(){
		BridgeSprite = GetNode<Sprite2D>("BridgeShape").GetNode<Sprite2D>("BridgeSprite");
	}
	public override void _PhysicsProcess(double delta){
		if(isActive){
			if(direction == 1){
				BridgeSprite.Position += new Vector2(480,0)*(float)delta;
				if(BridgeSprite.Position.X >= 0){
					Toggle();
					GD.Print("Pass");
					isActive = false;
				}
			}else if (direction == -1){
				BridgeSprite.Position += new Vector2(-480,0)*(float)delta;
				Toggle();
				if(BridgeSprite.Position.X <= -480){
					isActive = false;
				}
			}
		}
	}
	public void Toggle()
	{
		// ativar/desativar
		CollisionLayer = direction == 1 ? (uint)0 : (uint)1; // Habilita/Desabilita a colisão
		CollisionMask =  direction == 1 ? (uint)0 : (uint)1;

	}
	public void Activate(){
		isActive = true;
		direction = -direction;
		GD.Print("Activated");
	}

}
