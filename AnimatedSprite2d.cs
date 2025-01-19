using Godot;
using System;

public partial class AnimatedSprite2d : AnimatedSprite2D
{
	// Called when the node enters the scene tree for the first time.
	public int i = 0;
	public override void _Ready()
	{
		
		
		if(Animation.ToString() == "IdleBall"){
			var temp = GetParent();
			while(temp != null && temp.GetType().ToString() != "Phases") temp = temp.GetParent();
			if(temp != null){
				var Resized = ((Phases)temp).Resized;
				if(!Resized){
					var SpriteImage = SpriteFrames.GetFrameTexture(Animation, 0).GetImage();
					int scale = 80;
					SpriteImage.Resize(SpriteImage.GetWidth()*scale/100, SpriteImage.GetHeight()*scale/100, Image.Interpolation.Bilinear);
					SpriteFrames.SetFrame(Animation, 0, ImageTexture.CreateFromImage(SpriteImage));
					GD.Print(i);
					i++;
					((Phases)temp).Resized = true;
				}
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
