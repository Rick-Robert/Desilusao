using Godot;
using System;

public partial class Ponzo : Area2D
{
	public Area2D MinhaArea; public Player PlayerBody;
	private float InitialYPos, InitialPlayerScale, MaxPlayerScale, MinPlayerScale;
    private int times = 0;
	// Called when the node enters the scene tree for the first time.
	bool EstaNaArea = false;
    Texture2D PonzoSprite;
	public override void _Ready()
	{
		PonzoSprite = GetNode<Sprite2D>("Sprite2D").Texture;
		PlayerBody = GetParent().GetNode<Player>("Player");
        GD.Print("Player scale: ", PlayerBody.GetNode<Node2D>("Eye").Scale.X, " texture: ", PlayerBody.GetNode<Node2D>("Eye").GetNode<Sprite2D>("Outline").Texture.GetWidth());
        MaxPlayerScale = (PonzoSprite.GetWidth()*Scale.X)/(PlayerBody.GetNode<Node2D>("Eye").GetNode<Sprite2D>("Outline").Texture.GetWidth()*PlayerBody.GetNode<Node2D>("Eye").Scale.X);
        MaxPlayerScale *= (float)0.9;
        MinPlayerScale = (float)0.1*(float)MaxPlayerScale;
        GD.Print("MaxScale: ", MaxPlayerScale, " MinScale: ", MinPlayerScale);
	}

	public void OnBodyEntered(Node2D body) 
	{
        if(body.Name == "Player"){
		    EstaNaArea = true;
            InitialPlayerScale = body.Scale.Y;
            InitialYPos = body.GlobalPosition.Y;
            GD.Print("corpo entrou na area");
            //GD.Print("INitial: ", InitialYPos);
        }
	
	

	}

	public void OnBodyExited(Node2D body) 
	{
        if(body.Name == "Player"){
		    GD.Print("corpo saiu da area");
		    EstaNaArea = false;
        }
	}



	public override void _Process(double delta)
	{
		
		if (EstaNaArea)
		{
            //Area.Scale *= (float)1.03457; 10.3
            //Area.Scale *= (float)0.96657; 0.1
            var RelativePos = PlayerBody.GlobalPosition.Y - InitialYPos;
            GD.Print("Relative: ",RelativePos);
            PlayerBody.Scale = Vector2.One*(InitialPlayerScale+(float)RelativePos*MaxPlayerScale/(PonzoSprite.GetHeight()*Scale.Y));
            PlayerBody.Scale = PlayerBody.Scale.Min(Vector2.One*MaxPlayerScale);
            PlayerBody.Scale = PlayerBody.Scale.Max(Vector2.One*MinPlayerScale);
            if(PlayerBody.Scale == Vector2.One*MaxPlayerScale ||PlayerBody.Scale == Vector2.One*MinPlayerScale){
                InitialYPos = PlayerBody.GlobalPosition.Y;
                InitialPlayerScale = PlayerBody.Scale.Y;
            }
            //GD.Print("Bodyscale: ",PlayerBody.Scale);

		}

		

		
	}
}