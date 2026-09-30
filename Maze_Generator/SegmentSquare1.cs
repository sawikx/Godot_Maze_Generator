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

		float wallPositionY = 0.5f;
		float wallScaleY = 0.5f;  

		if (WallToResize[1] == 'u') //u->UP | position of wall
		{
			wallPositionY = 0.65f;
			wallScaleY = 0.6f;
		}
		else if (WallToResize[1] == 'd') //d->Down | position of wall
		{
			wallPositionY = 0.2f;
			wallScaleY = 0.3f;
		}
		else
		{
			wallPositionY = 0.5f;
			wallScaleY = 0.9f;
		}
		//l->Left | position of wall        
		//r->Richt | position of wall
		//m->Midle | position of wall

		if (WallToResize[0] == 'N')
		{
			float wallPositionX = (WallToResize[1] == 'r') ? 0.2f: (WallToResize[1] == 'l')?-0.2f : 0f;
			float wallScaleX = (WallToResize[1] == 'r' || WallToResize[1] == 'l')? 0.4f : (WallToResize[1] == 'm')?0.22f: 0.8f;

			float wallPositionZ = 0.45f;
			float wallScaleZ = 0.1f;

			WallN.Scale = new Vector3(wallScaleX, wallScaleY, wallScaleZ);
			WallN.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		else if (WallToResize[0] == 'S')
		{
			float wallPositionX = (WallToResize[1] == 'r') ? 0.2f : (WallToResize[1] == 'l') ? -0.2f : 0f;
			float wallScaleX = (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.4f : (WallToResize[1] == 'm') ? 0.22f : 0.8f;
			float wallPositionZ = -0.45f;
			float wallScaleZ = 0.1f;

			WallS.Scale = new Vector3(wallScaleX, wallScaleY, wallScaleZ);
			WallS.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		else if (WallToResize[0] == 'W')
		{
			float wallPositionX = 0.45f;
			float wallScaleX = 0.1f;
			float wallPositionZ = (WallToResize[1] == 'r') ? 0.2f : (WallToResize[1] == 'l') ? -0.2f : 0f;
			float wallScaleZ = (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.4f : (WallToResize[1] == 'm') ? 0.22f : 0.8f;

			WallW.Scale = new Vector3(wallScaleX, wallScaleY, wallScaleZ);
			WallW.Position = new Vector3(wallPositionX, wallPositionY, wallPositionZ);
		}
		else if (WallToResize[0] == 'E')
		{
			float wallPositionX = -0.45f;
			float wallScaleX = 0.1f;
			float wallPositionZ = (WallToResize[1] == 'r') ? 0.2f : (WallToResize[1] == 'l') ? -0.2f : 0f;
			float wallScaleZ = (WallToResize[1] == 'r' || WallToResize[1] == 'l') ? 0.4f : (WallToResize[1] == 'm') ? 0.22f : 0.8f;

			WallE.Scale = new Vector3(wallScaleX, wallScaleY, wallScaleZ);
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
