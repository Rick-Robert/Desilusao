using Godot;
using System;
using System.Xml.Resolvers;

public partial class MainMenu : MarginContainer
{
    // public Node nextScene;
    // public Node optScene;
    [Export] String NextSceneBegin;
    [Export] String NextSceneLevels;
    private Label _selectorOne;
    private Label _selectorTwo;
    private Label _selectorThree;
    private int currentSelection = 0;

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here.
        _selectorOne = GetNode<Label>("CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer/HBoxContainer/Selector");
        _selectorTwo = GetNode<Label>("CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer2/HBoxContainer/Selector");
        _selectorThree = GetNode<Label>("CenterContainer/VBoxContainer/CenterContainer2/VBoxContainer/CenterContainer3/HBoxContainer/Selector");
        // nextScene = ResourceLoader.Load<PackedScene>("res://level.tscn").Instantiate();
        // optScene = ResourceLoader.Load<PackedScene>("res://options.tscn").Instantiate();

        SetCurrentSelection(0);
    }
    public override void _Process(double delta)
    {
        if(Input.IsActionJustPressed("Resize")){
			GD.Print(DisplayServer.WindowGetMode());
			if(DisplayServer.WindowGetMode().Equals(DisplayServer.WindowMode.Fullscreen))
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
			else
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
		}
        // Called every time the node is added to the scene.
        // Initialization here.
        if(Input.IsActionJustPressed("Down") && currentSelection < 2){
            currentSelection++;
            SetCurrentSelection(currentSelection);
        } else if(Input.IsActionJustPressed("Up") && currentSelection > 0){
            currentSelection--;
            SetCurrentSelection(currentSelection);
        } else if(Input.IsActionJustPressed("Select")){
            handleSelection(currentSelection);
        }
    }

    public void handleSelection(int _currentSelection){
        if(_currentSelection == 0){
            // GetTree().Root.AddChild(nextScene);
            GetTree().ChangeSceneToFile(NextSceneBegin);
        } else if(_currentSelection == 1){
            GetTree().ChangeSceneToFile(NextSceneLevels);
        } else if(_currentSelection == 2){
            GetTree().Quit();
        }
    }

    public void SetCurrentSelection(int _currentSelection){
        _selectorOne.Text = "";
        _selectorTwo.Text = "";
        _selectorThree.Text = "";

        if(_currentSelection == 0){
            _selectorOne.Text = ">";
        } else if(_currentSelection == 1){
            _selectorTwo.Text = ">";
        } else if(_currentSelection == 2){
            _selectorThree.Text = ">";
        }
    }
    public void OnMouseEntered1(){
        currentSelection = 0;
        GD.Print(currentSelection);
        SetCurrentSelection(currentSelection);
        
    }
    public void OnMouseExited1(){

    }
    public void OnMouseEntered2(){
        currentSelection = 1;
        GD.Print(currentSelection);
        SetCurrentSelection(currentSelection);
    }
    public void OnMouseExited2(){

    }
    public void OnMouseEntered3(){
        currentSelection = 2;
        GD.Print(currentSelection);
        SetCurrentSelection(currentSelection);
    }
    public void OnMouseExited3(){

    }
}
