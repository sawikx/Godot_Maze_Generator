using Godot;
using System;

public partial class Player : CharacterBody3D
{    
	[Export]
	public bool can_move { get; set; } = true; //Alow player to input movment.

	[Export]
	public bool can_sprint { get; set; } = true; //Alow player to toggle sprint movment.

	[Export]
	public float move_speed { get; set; } = 8; //Players movement speed
	[Export]
	public float  move_speed_sprint { get; set; } = 16; //Players sprint movement speed
	[Export]
	public bool  move_sprint { get; set; } = false; //Player sprinting toggle
	[Export]
	public float  move_acceleration { get; set; } = 7; //Players acceleration to movment speed 
	[Export]
	public float  move_deacceleration { get; set; } = 10; //Players deacceleration from movment speed
	[Export]
	public bool  mouse_captured { get; set; } = true; //Toggles mouse captured mode
	[Export]
	public float  mouse_sensitivity_x { get; set; } = 0.3f; //Mouse sensitivity X axis
	[Export]
	public float  mouse_sensitivity_y { get; set; } = 0.3f; //Mouse sensitivity Y axis
	[Export]
	public float  mouse_max_up { get; set; } = 90; //Mouse max look angle up
	[Export]
	public float  mouse_max_down { get; set; } = -80; //Mouse max look angle down
	[Export]
	public float  Jump_speed { get; set; } = 6; //Players jumps speed
	[Export]
	public bool  allow_fall_input { get; set; } = true; //Alow player to input movment when falling
	[Export]
	public bool  stop_on_slope { get; set; } = false; //Toggle sliding on slopes
	[Export]
	public float max_slides { get; set; } = 4; //Maximum of slides
	[Export]
	public float  floor_max_angle { get; set; } = 60; //Maximum slop angle player can traverse
	[Export]
	public bool  infinite_inertia { get; set; } = false; //Toggle infinite inertia
	[Export]
	public float  gravaty { get; set; } = 9.81f; //Gravaty acceleration
	[Export]
	public Vector3  gravaty_vector { get; set; } = new Vector3(0, -1, 0); //Gravaty normal direction vector
	[Export]
	public Vector3  floor_normal { get; set; } = new Vector3(0, 1, 0); //Floor normal direction vector
	[Export]
	public Vector3  jump_vector { get; set; } = new Vector3(0, 1, 0); //Jump normal direction vector
	[Export]
	public Vector3  velocity { get; set; } = new Vector3(0, 0, 0); //Initial velocity


	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	public override void _Ready()
	{
		if (mouse_captured)
			{
				Input.SetMouseMode(Input.MouseModeEnum.Captured);
			}

	}

	public override void _UnhandledInput(InputEvent @event)
	{
	   /* if (_isPlayerDead())
		{
			return;
		} */
	   /*
		if (UseController)
		{
			if (@event is InputEventJoypadMotion eventJoypadMotion)
			{
				// Horizontal movement of head
				const float SensitivityControllerFactor = 5.0f;
				float controllerSensitivity = Sensitivity * SensitivityControllerFactor;
				if (eventJoypadMotion.Axis == JoyAxis.RightX)
				{
					controllerVector.X = -eventJoypadMotion.AxisValue * controllerSensitivity;
				}
				else if (eventJoypadMotion.Axis == JoyAxis.RightY)
				{
					controllerVector.Y = -eventJoypadMotion.AxisValue * controllerSensitivity;
				}
			}
		}
		else
		{*/
			if (@event is InputEventMouseMotion eventMouseMotion)
			{
			float Sensitivity = 0.004f;
			// Horizontal movement of head
			//GetNode<Camera3D>("Camera3D").Rotation.Y -= eventMouseMotion.Relative.X ;
			
			}
		//}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
