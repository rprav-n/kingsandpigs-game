using Godot;
using System;

public class Enemy : KinematicBody2D
{
	
	[Export]
	private int speed = 100;
	[Export]
	private int gravity = 500;
	
	private Vector2 enemyVelocity = Vector2.Zero;

	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
	{
		enemyMovement(delta);
	}
	
	public void enemyMovement(float delta) 
	{
		// apply gravity
		enemyVelocity += new Vector2(0, gravity * delta);
		if (enemyVelocity.y > 500) 
		{
			enemyVelocity.y = 500;
		}
		
		enemyVelocity = MoveAndSlide(enemyVelocity, Vector2.Up);
	}
}
