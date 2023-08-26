using Godot;
using System;

public class Player : KinematicBody2D
{

	// All export variables
	[Export]
	private int speed = 100;
	[Export]
	private int gravity = 500;
	[Export]
	private int jumpSpeed = 150;
	
	// All Normal variables
	private Vector2 playerVelocity = Vector2.Zero;
	private float playerDirection = 0;
	
	// All Node variables
	private AnimatedSprite animatedSprite;
	
	// All Scene variables
	

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	public override void _Process(float delta)
	{
		playerMovement(delta);
		updateAnimation();
	}
	
	private void playerMovement(float delta) 
	{
		// apply gravity
		playerVelocity += new Vector2(0, gravity * delta);
		if (playerVelocity.y > 500) 
		{
			playerVelocity.y = 500;
		}
		
		// movement
		playerDirection = Input.GetAxis("move_left", "move_right");
		
		playerVelocity.x = playerDirection * speed;
		
		playerJump(delta);

		playerVelocity = MoveAndSlide(playerVelocity, Vector2.Up);
	}
	
	private void playerJump(float delta) 
	{
		if (Input.IsActionJustPressed("jump")) 
		{
			playerVelocity.y = -jumpSpeed;
		}	
	}
	
	private void updateAnimation() 
	{
		
		if (IsOnFloor()) 
		{
			if (playerDirection != 0) 
			{
				animatedSprite.FlipH = playerDirection == -1;
				animatedSprite.Play("run");
			} else 
			{
				animatedSprite.Play("idle");
			}	
		} else 
		{
			if (playerDirection != 0) 
			{
				animatedSprite.FlipH = playerDirection == -1;	
			}
			animatedSprite.Play("jump");
		}
		
	}
}
