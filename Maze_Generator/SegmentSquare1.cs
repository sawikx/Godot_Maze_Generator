using Godot;
using System;

public partial class SegmentSquare1 : Node3D
{
	private StaticBody3D WallN ;
	private StaticBody3D WallS ;
	private StaticBody3D WallW ;
	private StaticBody3D WallE ;

	private bool WallNLock = false;
	private bool WallSLock = false;
	private bool WallWLock = false;
	private bool WallELock = false;

	public void RemoveWall(string WallToRemove)
	{
		if (IsItBlocked(WallToRemove)) return;

		WallN = GetNode<StaticBody3D>("WallN");
		WallS = GetNode<StaticBody3D>("WallS");
		WallW = GetNode<StaticBody3D>("WallW");
		WallE = GetNode<StaticBody3D>("WallE");

		if (WallToRemove == "N")
		{
			//GetNode<StaticBody3D>("WallN").Free();
			WallN.CollisionLayer = 0;
			WallN.CollisionMask = 0;
			WallN.Visible = false;
		}
		else if (WallToRemove == "S")
		{
			//GetNode<StaticBody3D>("WallS").Free();
			WallS.CollisionLayer = 0;
			WallS.CollisionMask = 0;
			WallS.Visible = false;
		}
		else if (WallToRemove == "W")
		{
			//GetNode<StaticBody3D>("WallW").Free();
			WallW.CollisionLayer = 0;
			WallW.CollisionMask = 0;
			WallW.Visible = false;
		}
		else if (WallToRemove == "E")
		{
			//GetNode<StaticBody3D>("WallE").Free();
			WallE.CollisionLayer = 0;
			WallE.CollisionMask = 0;
			WallE.Visible = false;
		}
		else if (WallToRemove == "ALL")
		{			         
		
		}
		
	}

	public void ResizeWall(string WallToResize)
	{
		if (IsItBlocked(WallToResize)) return;

		WallN = GetNode<StaticBody3D>("WallN");
		WallS = GetNode<StaticBody3D>("WallS");
		WallW = GetNode<StaticBody3D>("WallW");
		WallE = GetNode<StaticBody3D>("WallE");

		float wallPositionY = WallN.Position.Y;
		float wallScaleY = WallN.Position.Y;  

		if (WallToResize[1] == 'u') //u->UP | position of wall
		{
			wallPositionY = 0.65f;
			wallScaleY = 0.6f;
		}
		if (WallToResize[1] == 'd') //d->Down | position of wall
		{
			wallPositionY = 0.2f;
			wallScaleY = 0.3f;
		}
		 //l->Left | position of wall        
		 //r->Richt | position of wall
		

		if (WallToResize[0] == 'N')
		{
			float wallPositionX = (WallToResize[1] == 'r') ? 0.2f: (WallToResize[1] == 'l')?-0.2f :WallN.Position.X;
			float wallScaleX = (WallToResize[1] == 'r' || WallToResize[1] == 'l')? 0.4f: WallN.Scale.X;

			float wallPositionZ = WallN.Position.Z;
			float wallScaleZ = WallN.Scale.Z;

			WallN.Scale = new Vector3(wallScaleX, (WallToResize[1] == 'r' || WallToResize[1] == 'l') ?0.9f:wallScaleY, wallScaleZ);
			WallN.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		else if (WallToResize[0] == 'S')
		{
			float wallPositionX = (WallToResize[1] == 'r') ? 0.2f : (WallToResize[1] == 'l') ? -0.2f : WallS.Position.X;
			float wallScaleX = (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.4f : WallS.Scale.X;
			float wallPositionZ = WallS.Position.Z;
			float wallScaleZ = WallS.Scale.Z;

			WallS.Scale = new Vector3(wallScaleX, (WallToResize[1] == 'r' || WallToResize[1] == 'l')? 0.9f : wallScaleY, wallScaleZ);
			WallS.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		else if (WallToResize[0] == 'W')
		{
			float wallPositionX = WallW.Position.X;
			float wallScaleX = WallW.Scale.X;
			float wallPositionZ = (WallToResize[1] == 'r') ? 0.2f : (WallToResize[1] == 'l') ? -0.2f : WallW.Position.Z;
			float wallScaleZ = (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.4f : WallW.Scale.Z;

			WallW.Scale = new Vector3(wallScaleX, (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.9f : wallScaleY, wallScaleZ);
			WallW.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		else if (WallToResize[0] == 'E')
		{
			float wallPositionX = WallE.Position.X;
			float wallScaleX = WallE.Scale.X;
			float wallPositionZ = (WallToResize[1] == 'r') ? 0.2f : (WallToResize[1] == 'l') ? -0.2f : WallE.Position.Z;
			float wallScaleZ = (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.4f : WallE.Scale.Z;

			WallE.Scale = new Vector3(wallScaleX, (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.9f : wallScaleY, wallScaleZ);
			WallE.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		
	}

	public void PositionSpawn(Vector3 WerePosition)
	{
		Position= WerePosition;
	}

	private bool IsItBlocked(string wall)
	{
		if (WallNLock && wall[0] == 'N')
		{
			return true;
		}
		if (WallSLock && wall[0] == 'S')
		{
			return true;
		}
		if (WallWLock && wall[0] == 'W')
		{
			return true;
		}
		if (WallELock && wall[0] == 'E')
		{
			return true;
		}

		return false;
	}

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
	}

}
