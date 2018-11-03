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
    void Init(Vector2 dir) 
    {
        direction = dir.normalized;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            BounceOffPlayer(collision);
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

    // Bounce off a player. Precondition is that c must be a player.
    private void BounceOffPlayer(Collider2D c) 
    {
        Debug.Log("BounceOffPlayer called");
        // Get the direction to fly off of
        Vector3 diff = transform.position - c.transform.position;
        direction = new Vector2(diff.x, diff.y).normalized;
    }

    public float Speed { get { return speed; } set { speed = value; } }
}
