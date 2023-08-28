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
	private bool isAttacking = false;
	
	// All Normal variables
	private Vector2 playerVelocity = Vector2.Zero;
	private float playerDirection = 0;
	
	// All Node variables
	private AnimatedSprite animatedSprite;
	private CollisionShape2D collisionShape2D;
	
	// All Scene variables
	

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
	}

	public override void _PhysicsProcess(float delta)
	{
		base._PhysicsProcess(delta);
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
		if (Input.IsActionJustPressed("attack")) 
		{
			isAttacking = true;
			animatedSprite.Play("attack");
		} else if (!isAttacking)
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
		var collisionPosition = new Vector2(-7, 2);
		if (animatedSprite.FlipH) 
		{
			collisionPosition.x = 7;
		}
		collisionShape2D.Position = collisionPosition;
	}
	
	private void _on_AnimatedSprite_animation_finished() 
	{
		isAttacking = false;
		//animatedSprite.Play("idle");
	}
}
