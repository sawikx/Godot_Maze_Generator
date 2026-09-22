using Godot;
using System;
using System.Linq;
using System.Collections.Generic;

public partial class Main : Node
{

	[Signal]
	delegate void StartGameEventHandler();

	[Signal]
	delegate void EndGameEventHandler();

	[Export]
	public PackedScene Segment1 { get; set; }

	[Export]
	public int SizeOfMaze { get; set; } = 5;

	#region MazeCreate
	public void CreatingMaze(ulong seed = 2)
	{		
		int SideLengthMaze = SizeOfMaze;
		var random = new RandomNumberGenerator();
		//random.Seed = seed;		
		var NumberOfBlocksInMaze = SideLengthMaze * SideLengthMaze;
		var StartLocation = random.Randi() % NumberOfBlocksInMaze;
		//GD.Print(StartLocation);        
		List<int> RoadReturn = new List<int>();		
		var Move  = StartLocation;

		List<int> MetaPosibliliti = new List<int>();
		bool MetaPosiblilitiAdded = true;

		int ile = 0;
		Dictionary<long, List<string>> Road = new Dictionary<long, List<string>>();
		while (Road.Count < NumberOfBlocksInMaze)
		{
			if (RoadReturn.Count == 0 && Road.Count != 0)
			{				
				break;
			}
			List<string> RoadPossible = new List<string>();
			if (Move + SideLengthMaze <= NumberOfBlocksInMaze - 1)
			{
				if (!Road.ContainsKey(Move + SideLengthMaze))//N
				{
					RoadPossible.Add("N");
					 
				}				
			}
			if (Move - SideLengthMaze >= 0)
			{
				if (!Road.ContainsKey(Move - SideLengthMaze))//S
				{
					RoadPossible.Add("S");
					 
				}                
			}
			if (Move % SideLengthMaze +1 != SideLengthMaze)
			{
				if (!Road.ContainsKey(Move + 1))//W
				{
					RoadPossible.Add("W");
					 
				}                
			}			
			if (Move % SideLengthMaze != 0)
			{
				if (!Road.ContainsKey(Move - 1))//E
				{
					RoadPossible.Add("E");
					
				}                
			}
			//GD.Print();
			if (RoadPossible.Count == 0)//Beck
			{
				//GD.Print();
				if (MetaPosiblilitiAdded)
				{
					MetaPosibliliti.Add(unchecked((int)Move));
					MetaPosiblilitiAdded = false;
				}
				
				Move = RoadReturn[RoadReturn.Count - 1];
				RoadReturn.Remove(unchecked((int)Move));				
			}			
			else
			{
				MetaPosiblilitiAdded = true;
				string Directions = RoadPossible[GD.RandRange(0, RoadPossible.Count() - 1)];
				RoadPossible.Remove(Directions);
				//GD.Print();
				AddOrUpdate(Road, Move, Directions);
								
				RoadReturn.Add(unchecked((int)Move));
				//GD.Print();
				if (Directions == "N")
				{
					Move += SideLengthMaze;
				}
				else if (Directions == "S")
				{
					Move -= SideLengthMaze;
				}
				else if (Directions == "W")
				{
					Move++;
				}
				else if (Directions == "E")
				{
					Move--;
				}
				AddOrUpdate(Road, Move, Reverser(Directions));
			}
			ile++;
		}
		if (MetaPosibliliti.Count == 0)
		{
			MetaPosibliliti.Add(unchecked((int)Move));
		}
		//room creation
		//GD.Print("---------");
		int MetaPosition = MetaPosibliliti[GD.RandRange(0, MetaPosibliliti.Count() - 1)];

		for (int ii = 0; ii < SideLengthMaze; ii++)
		{
			for (int jj = 0; jj < SideLengthMaze; jj++)
			{
				SegmentSquare1 segment1 = Segment1.Instantiate<SegmentSquare1>();
				var lokalizacjaPokoj = new Vector3(jj *2, 0, ii *2);
				segment1.PositionSpawn(lokalizacjaPokoj);
				if (ii * SideLengthMaze + jj == StartLocation)
				{
					GetNode<CharacterBody3D>("Player").Position = new Vector3(jj*2, 1f, ii*2);
				}
				if (ii * SideLengthMaze + jj == MetaPosition)
				{
					GetNode<Area3D>("Meta").Position = new Vector3(jj * 2, 0.1f, ii * 2);
				}
				foreach (var i in Road[ii*SideLengthMaze+jj]) 
				{
					//GD.Print(ii * SideLengthMaze + jj +" wall -> "+i);
					segment1.RemoveWall(i);
				}
					
				AddChild(segment1);
				//GD.Print("T"+i+" "+j);							
			}
		}
	}

	private void AddOrUpdate(Dictionary<long, List<string>> targetDictionary, long key, string entry)
	{
		if (!targetDictionary.ContainsKey(key))
		{
			targetDictionary.Add(key, new List<string>());
		}

		targetDictionary[key].Add(entry);
	}

	private string Reverser(string Directions)
	{
		if (Directions == "N")
		{
			return  "S";
		}
		else if (Directions == "S")
		{
			return "N";
		}
		else if (Directions == "W")
		{
			return "E";
		}
		else if (Directions == "E")
		{
			return  "W";
		}
		else
		{
			return "";
		}
	}
	#endregion
	
	public void on_start_pressed()
	{
		CreatingMaze();
		EmitSignal(SignalName.StartGame);
		Input.MouseMode = Input.MouseModeEnum.Hidden;
		Input.MouseMode = Input.MouseModeEnum.Captured;		
	}

	public void on_meta_body_entered(Node3D body)
	{
		if (body.Name == "Player")
		{
			EmitSignal(SignalName.StartGame);
			Input.MouseMode = Input.MouseModeEnum.Visible;
			EmitSignal(SignalName.EndGame);
			GetTree().CallGroup("mazeblock", Node.MethodName.QueueFree);
		}
		
	}

	public void on_menu_maze_size_change(string NewMazeSize)
	{
		SizeOfMaze = int.Parse(NewMazeSize);
	}

 // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	   
	}
}
