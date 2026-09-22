using Godot;
using System;

public partial class Menu : CanvasLayer
{
	[Signal]
	public delegate void StartGameEventHandler();

	[Signal]
	public delegate void MazeSizeChangeEventHandler(string new_size);

	public void on_start_pressed() 
	{
		EmitSignal(SignalName.StartGame);
		GetNode<Control>("Control").Visible = !GetNode<Control>("Control").Visible;
	}

	public void EndGame_event()
	{
		GetNode<Control>("Control").Visible = !GetNode<Control>("Control").Visible;
	}

	public void on_maze_size_text_submitted(string NewMazeSize)
	{
		EmitSignal(SignalName.MazeSizeChange, NewMazeSize);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
