using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
    // The rigidbody of the ball
    Rigidbody2D rigidbody;
    // Determine the direction of the ball
    private Vector2 direction = Vector2.up;
    // The movement speed of the ball
    private float speed = 3;

    // Use this for initialization
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
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

    // Set the rotation of the ball
    public void SetRotation(float z) 
    {
        transform.rotation = Quaternion.Euler(0, 0, z);
    }

    // Move the ball
    public void Move()
    {
        rigidbody.velocity = direction * speed;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("Entered");
    //    if (collision.gameObject.CompareTag("Player")) {
    //        BounceOffPlayer(collision);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered Trigger. Tag: " + collision.tag);
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Spawner"))
        {
            BounceOffRound(collision);
        }
        if (collision.gameObject.CompareTag("Wall")) 
        {
            BounceOffWall(collision);
        }
        if (collision.gameObject.CompareTag("Despawner"))
        {
            Destroy(this.gameObject);
        }
    }

    // Bounce off a player. Precondition is that c must be a wall.
    private void BounceOffWall(Collider2D c)
    {
        Debug.Log("BounceOffWall called");
        // Get the direction to fly off of
        bool vertical = c.gameObject.GetComponent<Wall>().IsVertical;

        // Horizontal - mirror the x direction
        if (vertical) {
            direction.x = -direction.x;
        // Vertical - mirror the y direction
        } else {
            direction.y = -direction.y;
        }
    }

    // Bounce off a player. Precondition is that c must be a player or spawnerß.
    private void BounceOffRound(Collider2D c) 
    {
        Debug.Log("BounceOffRound called");
        // Get the direction to fly off of
        Vector3 diff = transform.position - c.transform.position;
        direction = new Vector2(diff.x, diff.y).normalized;
    }

    public float Speed { get { return speed; } set { speed = value; } }
}
