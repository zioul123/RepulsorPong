using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
    // The rigidbody of the ball
    Rigidbody2D rigidbody;
    // Collider of the ball
    Collider2D collider;
    // Determine the direction of the ball
    private Vector2 direction = Vector2.up;
    // The movement speed of the ball
    private float speed = 200f;

    // Use this for initialization
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Initialize the direction
    public void Init(Vector2 dir, float speed) 
    {
        direction = dir.normalized;
        this.speed = speed;
    }
	
	void FixedUpdate() 
    {
        Move();
	}

    // Move the ball
    public void Move()
    {
        rigidbody.velocity = direction * speed * Time.deltaTime;
    }

    // Handle collisions of the ball
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entered Collision");
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Spawner"))
        {
            BounceOffRound(collision.collider);
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            BounceOffWall(collision.collider);
        }
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Let balls phase through each other
            Physics2D.IgnoreCollision(collider, collision.collider);
        }
        if (collision.gameObject.CompareTag("Despawner"))
        {
            collision.gameObject.GetComponent<Despawner>().MinusOne();
            Destroy(this.gameObject);
        }
    }

    // Bounce off a player. Precondition is that c must be a wall.
    private void BounceOffWall(Collider2D c)
    {
        Debug.Log("BounceOffWall called");

        // Check if the wall is going to bounce it horizontally or vertically
        bool vertical = c.gameObject.GetComponent<Wall>().IsVertical;

        // Horizontal - mirror the x direction
        if (vertical) 
        {
            direction.x = -direction.x;
        }
        // Vertical - mirror the y direction
        else
        {
            direction.y = -direction.y;
        }
    }

    // Bounce off a player. Precondition is that c must be a player or spawnerß.
    private void BounceOffRound(Collider2D c) 
    {
        Debug.Log("BounceOffRound called");

        // Get the direction to fly off of
        Vector3 diff = transform.position - c.transform.position;
        // Set the direction
        direction = new Vector2(diff.x, diff.y).normalized;
    }

    public float Speed { get { return speed; } set { speed = value; } }
}
