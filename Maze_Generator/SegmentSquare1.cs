using Godot;
using System;

public partial class SegmentSquare1 : Node3D
{
	public void RemoveWall(string WallToRemove)
	{
		if (WallToRemove == "N")
		{
			GetNode<StaticBody3D>("WallN").Free();
		}
		else if (WallToRemove == "S")
		{
			GetNode<StaticBody3D>("WallS").Free();
		}
		else if (WallToRemove == "W")
		{
			GetNode<StaticBody3D>("WallW").Free();
		}
		else if(WallToRemove == "E")
		{
			GetNode<StaticBody3D>("WallE").Free();
		}
	}

	public void PositionSpawn(Vector3 WerePosition)
	{
		Position= WerePosition;
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

}
