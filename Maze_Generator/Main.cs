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

			float randomFloat = GD.Randf(); //chance to different wall versions
			float[] randomtablechance = [0.70f,0.76f,0.90f,0.95f];


			List<string> RoadPossible = new List<string>();
			if (Move + SideLengthMaze <= NumberOfBlocksInMaze - 1)
			{
				if (!Road.ContainsKey(Move + SideLengthMaze))//N
				{
					if (randomFloat < randomtablechance[0])
					{
						RoadPossible.Add("N");
					}
					else if (randomFloat < randomtablechance[1])
					{
						RoadPossible.Add("Nu");
					}
					else if (randomFloat < randomtablechance[2])
					{
						RoadPossible.Add("Nd");
					}
					else if(randomFloat < randomtablechance[3])
					{
						RoadPossible.Add("Nl");
					}
					else
					{
						RoadPossible.Add("Nr");
					}
					 
				}				
			}
			if (Move - SideLengthMaze >= 0)
			{
				if (!Road.ContainsKey(Move - SideLengthMaze))//S
				{
					if (randomFloat < randomtablechance[0])
					{
						RoadPossible.Add("S");
					}
					else if (randomFloat < randomtablechance[1])
					{
						RoadPossible.Add("Su");
					}
					else if (randomFloat < randomtablechance[2])
					{
						RoadPossible.Add("Sd");
					}
					else if (randomFloat < randomtablechance[3])
					{
						RoadPossible.Add("Sl");
					}
					else
					{
						RoadPossible.Add("Sr");
					}

				}                
			}
			if (Move % SideLengthMaze +1 != SideLengthMaze)
			{
				if (!Road.ContainsKey(Move + 1))//W
				{
					if (randomFloat < randomtablechance[0])
					{
						RoadPossible.Add("W");
					}
					else if (randomFloat < randomtablechance[1])
					{
						RoadPossible.Add("Wu");
					}
					else if (randomFloat < randomtablechance[2])
					{
						RoadPossible.Add("Wd");
					}
					else if (randomFloat < randomtablechance[3])
					{
						RoadPossible.Add("Wl");
					}
					else
					{
						RoadPossible.Add("Wr");
					}

				}                
			}			
			if (Move % SideLengthMaze != 0)
			{
				if (!Road.ContainsKey(Move - 1))//E
				{
					if (randomFloat < randomtablechance[0])
					{
						RoadPossible.Add("E");
					}
					else if (randomFloat < randomtablechance[1])
					{
						RoadPossible.Add("Eu");
					}
					else if (randomFloat < randomtablechance[2])
					{
						RoadPossible.Add("Ed");
					}
					else if (randomFloat < randomtablechance[3])
					{
						RoadPossible.Add("El");
					}
					else
					{
						RoadPossible.Add("Er");
					}

				}                
			}
			//GD.Print();
			if (RoadPossible.Count == 0)//return
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
				//GD.Print(Directions +" "+ Reverser(Directions));
				AddOrUpdate(Road, Move, Directions);
								
				RoadReturn.Add(unchecked((int)Move));
				//GD.Print();
				if (Directions[0] == 'N')
				{
					Move += SideLengthMaze;
				}
				else if (Directions[0] == 'S')
				{
					Move -= SideLengthMaze;
				}
				else if (Directions[0] == 'W')
				{
					Move++;
				}
				else if (Directions[0] == 'E')
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
					//GetNode<CharacterBody3D>("Player").Position = new Vector3(jj*2, 1f, ii*2);
					//GD.Print(jj+"x,  z"+ii+" try:"+ StartLocation% SideLengthMaze+" "+ StartLocation/SideLengthMaze);
				}
				if (ii * SideLengthMaze + jj == MetaPosition)
				{
					GetNode<Area3D>("Meta").Position = new Vector3(jj * 2, 0.1f, ii * 2);
				}
				//GD.Print(randomFloat+".");

				bool whenup = (GD.Randi() % 2 == 0) ? true : false;

				foreach (var i in Road[ii * SideLengthMaze + jj])
				{
					//GD.Print(ii * SideLengthMaze + jj +" wall -> "+i);
					if (i.Length == 1)
					{
						segment1.RemoveWall(i);
					}
					else
					{
						segment1.ResizeWall(i);

					}
					
				}			
					
				AddChild(segment1);
				//GD.Print("T"+i+" "+j);							
			}
		}
		GetNode<CharacterBody3D>("Player").Position = new Vector3(StartLocation % SideLengthMaze * 2, 1f, StartLocation / SideLengthMaze * 2);
	}

	private void AddOrUpdate(Dictionary<long, List<string>> targetDictionary, long key, string entry)
	{
		if (!targetDictionary.ContainsKey(key))
		{
			targetDictionary.Add(key, new List<string>());
		}

		targetDictionary[key].Add(entry);
	}

	private string Reverser(string directions)
	{
		string Directions = directions;
		if (Directions[0] == 'N')
		{			
			return Directions.Replace('N', 'S');
		}
		else if (Directions[0] == 'S')
		{
			
			return Directions.Replace('S', 'N');
		}
		else if (Directions[0] == 'W')
		{
			
			return Directions.Replace('W', 'E');
		}
		else if (Directions[0] == 'E')
		{
			
			return Directions.Replace('E', 'W');
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

		if (GetNode<CharacterBody3D>("Player").Position.Y < -100f)
		{
			GetNode<CharacterBody3D>("Player").Position = new Vector3(GetNode<CharacterBody3D>("Player").Position.X, 0.5f, GetNode<CharacterBody3D>("Player").Position.Z);
		}
	}
}
