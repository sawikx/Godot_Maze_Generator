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

	[Signal]
	delegate void CameraChangeEventHandler();

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
		List<long> Road = new List<long>();
		var Move  = StartLocation;
		
		List<int> MetaPosibliliti = new List<int>();
		List<float> MetaPosiblilitiProbabilities = new List<float>();
		bool MetaPosiblilitiAdded = true;

		int ile = 0;
		Dictionary<long, List<string>> SegmentAppearance = new Dictionary<long, List<string>>();
		
		while (SegmentAppearance.Count < NumberOfBlocksInMaze)
		{			
			if (RoadReturn.Count == 0 && SegmentAppearance.Count != 0)
			{				
				break;
			}            

			float randomFloat = GD.Randf(); //chance to different wall verswions
			float[] randomtablechance = [0.70f, 0.75f, 0.85f, 0.90f, 0.95f];//[0.70f,0.75f,0.85f,0.90f,0.95f] [0.10f,0.15f,0.25f,0.30f,0.35f]

			List<string> RoadPossible = new List<string>();
			if (Move + SideLengthMaze <= NumberOfBlocksInMaze - 1)
			{
				if (!Road.Contains(Move + SideLengthMaze))//N
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
					else if (randomFloat < randomtablechance[4])
					{
						RoadPossible.Add("Nr");
					}
					else
					{
						RoadPossible.Add("Nm");
					}
					 
				}				
			}
			if (Move - SideLengthMaze >= 0)
			{
				if (!Road.Contains(Move - SideLengthMaze))//S
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
					else if (randomFloat < randomtablechance[4])
					{
						RoadPossible.Add("Sr");
					}
					else
					{
						RoadPossible.Add("Sm");
					}

				}                
			}
			if (Move % SideLengthMaze +1 != SideLengthMaze)
			{
				if (!Road.Contains(Move + 1))//W
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
					if (randomFloat < randomtablechance[4])
					{
						RoadPossible.Add("Wr");
					}
					else
					{
						RoadPossible.Add("Wm");
					}

				}                
			}			
			if (Move % SideLengthMaze != 0)
			{
				if (!Road.Contains(Move - 1))//E
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
					if (randomFloat < randomtablechance[4])
					{
						RoadPossible.Add("Er");
					}
					else
					{
						RoadPossible.Add("Em");
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
					MetaPosiblilitiProbabilities.Add(RoadReturn.Count);
					MetaPosiblilitiAdded = false;
				}
				
				Move = RoadReturn[RoadReturn.Count - 1];
				RoadReturn.Remove(unchecked((int)Move));				
			}			
			else
			{				
				if (!Road.Contains(Move)) Road.Add(Move);
				MetaPosiblilitiAdded = true;
				string Directions = RoadPossible[GD.RandRange(0, RoadPossible.Count() - 1)];
				RoadPossible.Remove(Directions);
				//GD.Print(Directions +" "+ Reverser(Directions));
				AddOrUpdate(SegmentAppearance, Move, Directions);			
				RoadReturn.Add(unchecked((int)Move));

				if (RoadPossible.Count > 1)
				{
					float SegmentAdditionalToRemove = GD.Randf(); //chance to different wall versions
					float[] SegmentAdditionalToRemoveRandomTableChance = [0.85f, 0.95f, 0.99f]; // 0 - not, 1 - one more wall ...
																								//int[] SegmentAdditionalToRemove = [1, 2, 3];
					int SegmentAdditional;

					if (SegmentAdditionalToRemove < SegmentAdditionalToRemoveRandomTableChance[0])
					{
						SegmentAdditional = 0;
					}
					else if (SegmentAdditionalToRemove < SegmentAdditionalToRemoveRandomTableChance[1])
					{
						SegmentAdditional = 1;
					}
					else if (SegmentAdditionalToRemove < SegmentAdditionalToRemoveRandomTableChance[2])
					{
						SegmentAdditional = 2;
					}
					else
					{
						SegmentAdditional = 3;
					}

					if (RoadPossible.Count < SegmentAdditional) 
					{
						SegmentAdditional = RoadPossible.Count;
					}

					for (int i1 = 0; i1 < SegmentAdditional; i1++)
					{
						long MoveAdditional = Move;
						//GD.Print(GD.RandRange(0, RoadPossible.Count() - 1) + " los | " + RoadPossible.Count() + " cout|  ile+" + SegmentAdditional);
						string Directions2 = RoadPossible[GD.RandRange(0, RoadPossible.Count() - 1)];
						RoadPossible.Remove(Directions2);
						AddOrUpdate(SegmentAppearance, Move, Directions2);
						//GD.Print(MoveAdditional + " "+Directions2);

						if (Directions2[0] == 'N')
						{
							MoveAdditional += SideLengthMaze;
						}
						else if (Directions2[0] == 'S')
						{
							
							MoveAdditional -= SideLengthMaze;
						}
						else if (Directions2[0] == 'W')
						{                            
							MoveAdditional++;
						}
						else if (Directions2[0] == 'E')
						{                            
							MoveAdditional--;
						}
						//GD.Print(MoveAdditional + " " + Reverser(Directions2));
						AddOrUpdate(SegmentAppearance, MoveAdditional, Reverser(Directions2));

					}

				}
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
				AddOrUpdate(SegmentAppearance, Move, Reverser(Directions));
				Road.Add(Move);
			}
			ile++;
		}
		if (MetaPosibliliti.Count == 0)
		{
			MetaPosibliliti.Add(unchecked((int)Move));
			MetaPosiblilitiProbabilities.Add(1);
		}
		//room creation
		//GD.Print("---------");		
		int gfrh = unchecked((int)random.RandWeighted(MetaPosiblilitiProbabilities.ToArray()));
		int MetaPosition = MetaPosibliliti[unchecked((int)random.RandWeighted(MetaPosiblilitiProbabilities.ToArray()))];

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

				foreach (var i in SegmentAppearance[ii * SideLengthMaze + jj])
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
		if(int.TryParse(NewMazeSize,out int res)) SizeOfMaze = int.Parse(NewMazeSize);
	}

 // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Visible;
		
	}

	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("camera"))
		{
			//GD.Print("tak " + GetNode<Camera3D>("Marker3D/Camera3D").Current);
			if (GetNode<Camera3D>("Marker3D/Camera3D").Current)
			{
				EmitSignal(SignalName.CameraChange);
			}
			else
			{
				EmitSignal(SignalName.StartGame);
				GetNode<Camera3D>("Marker3D/Camera3D").MakeCurrent();
			}
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if (GetNode<CharacterBody3D>("Player").Position.Y < -100f)
		{
			GetNode<CharacterBody3D>("Player").Position = new Vector3(GetNode<CharacterBody3D>("Player").Position.X, 0.5f, GetNode<CharacterBody3D>("Player").Position.Z);
		}

		if (GetNode<Camera3D>("Marker3D/Camera3D").Current)
		{
			float speedc =  0.05f;

			if (Input.IsActionPressed("move_left"))
			{
				GetNode<Marker3D>("Marker3D").Position -= new Vector3(speedc, 0, 0);
			}
			if (Input.IsActionPressed("move_right"))
			{
				GetNode<Marker3D>("Marker3D").Position += new Vector3(speedc, 0,  0);
			}
			if (Input.IsActionPressed("move_forward"))
			{
				GetNode<Marker3D>("Marker3D").Position -= new Vector3( 0, 0, speedc);
			}
			if (Input.IsActionPressed("move_back"))
			{
				GetNode<Marker3D>("Marker3D").Position += new Vector3(0, 0, speedc);
			}
		}

	}
}
