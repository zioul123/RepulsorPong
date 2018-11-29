using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player 0, 1, 2, or 3, for key controls
    [SerializeField]
    private int playerNumber;
    // Store the player's controls in order right, left, speed, repel
    private KeyCode[] controls = new KeyCode[4];
    // Speed of the player
    [SerializeField]
    private float speed = 6;
    // Rigidbody of the player
    private Rigidbody2D rigidbody2d;
    // The repel ring object for repel animation
    [SerializeField]
    private Transform repelRing;
    // The repelRing's animator
    private Animator repelRingAnimator;

    // For movement purposes
    // Direction of the player
    private Vector2 direction = Vector2.zero;
    // Whether the speed button is pressed
    private bool speedUp;

    // For repel purposes
    private Coroutine charging;

    // Use this for initialization
    void Start()
    {
        SetControls();
        rigidbody2d = GetComponent<Rigidbody2D>();
        repelRingAnimator = repelRing.GetComponent<Animator>();
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
            Repel();
        }
    }

    // Repel all balls away from the paddle
    private void Repel()
    {
        // Only repel if not charging right now
        if (charging == null)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.3f);
            foreach (Collider2D collider in colliders)
            {
                // Repel all balls away at double speed
                if (collider.CompareTag("Ball"))
                {
                    Ball ball = collider.GetComponent<Ball>();
                    ball.RepelFrom(GetComponent<Collider2D>());
                }
            }

            // Set repel animation to start and reset "Charged"
            repelRingAnimator.ResetTrigger("Charged");
            repelRingAnimator.SetTrigger("Repel");
            
            // Start charging
            charging = StartCoroutine(Charge()); 
        }
    }

    // The coroutine used after repelling
    private IEnumerator Charge()
    {
        yield return new WaitForSeconds(2);
        // Set repel animation to ready and reset "Repel"
        repelRingAnimator.ResetTrigger("Repel");
        repelRingAnimator.SetTrigger("Charged"); 
        // Clear the coroutine
        charging = null;
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
