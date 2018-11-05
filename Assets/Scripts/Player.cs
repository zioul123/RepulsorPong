using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player 0, 1, 2, or 3, for key controls
    [SerializeField]
    private int playerNumber;
    // Store the player's controls in order right, left, speed, repel
    private readonly KeyCode[] controls = new KeyCode[4];
    // Speed of the player
    [SerializeField]
    private float speed = 6;
    // Rigidbody of the player
    private Rigidbody2D rigidbody2d;

    // For movement purposes
    // Direction of the player
    private Vector2 direction = Vector2.zero;
    // Whether the speed button is pressed
    private bool speedUp;

    // Use this for initialization
    void Start()
    {
        SetControls();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidbody2d.velocity = Vector2.zero;
        RegisterInput();
        Move();
    }

    // Register any key input
    private void RegisterInput()
    {
        // Reset movement variables
        direction = Vector2.zero; 
        speedUp = false;

        if (Input.GetKey(controls[0])) // Right
        {
            direction += Vector2.right;
        }
        if (Input.GetKey(controls[1])) // Left
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(controls[2])) // Speed
        {
            speedUp = true;
        }
        if (Input.GetKey(controls[3])) // Repel
        {

        }
    }

    // Move based on direction
    private void Move() 
    {
        rigidbody2d.velocity = direction * speed * (speedUp ? 2f : 1f) * Time.deltaTime;
    }

    // Set the controls based on player number
    private void SetControls()
    {
        // Red player's controls
        if (playerNumber == 0)
        {
            controls[0] = KeyCode.S; // Right
            controls[1] = KeyCode.A; // Left
            controls[2] = KeyCode.LeftShift; // Speed
            controls[3] = KeyCode.Space; // Repel
        }

        // Blue player's controls
        if (playerNumber == 1)
        {
            controls[0] = KeyCode.RightArrow;
            controls[1] = KeyCode.LeftArrow;
            controls[2] = KeyCode.Return;
            controls[3] = KeyCode.RightShift;
        }
    }
}
