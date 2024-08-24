using Godot;
using System;

public partial class jogador : CharacterBody3D
{
	[Signal]
	public delegate void MorreuEventHandler();
	
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public AnimationPlayer animations = null;
	
	private bool morreu = false;

	[Export]
	public bool GodMode { get; set; }	

	public override void _Ready()
	{
		animations = GetNode<AnimationPlayer>("AnimationPlayer");
	}
	
	public void Resetar() 
	{
		SetPhysicsProcess(true);
		animations.Play("RESET");
		morreu = false;
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

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
	private async void _on_collisor_inimigo_body_entered(Node3D body)
	{
		if (morreu || GodMode) {
			return;
		}
		
		morreu = true;
		
		SetPhysicsProcess(false);
		
		animations.Play("morreu");
		await ToSignal(animations,"animation_finished");
	
		EmitSignal("Morreu");
	}

	private void _on_collisor_moedas_area_entered(Area3D body)
	{	
		body.QueueFree();
	}

}
