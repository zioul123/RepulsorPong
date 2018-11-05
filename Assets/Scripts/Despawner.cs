using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Delegates for Observers
// Called when a ball is despawned
public delegate void NotifyDespawnBall();
// Called when the person's life hits 0
public delegate void NotifyGameEnd();

public class Despawner : MonoBehaviour 
{
    // The Text that shows the number of balls left
    [SerializeField]
    private Text ballsLeftText;
    // Initial number of balls
    [SerializeField]
    private int ballsStart = 30;
    // Used to track number of balls left
    private int currentBalls;
    // Stop the despawner from subtracting scores when game ends
    public bool Stop { get; set; }

    // Observer callbacks
    public event NotifyDespawnBall NotifyDespawnBall;
    public event NotifyGameEnd NotifyGameEnd;

    // Use this for initialization
    void Start() 
    {
        Stop = false;
        ballsLeftText.text = ballsStart + "/" + ballsStart;
        currentBalls = ballsStart;
	}
	
    // Minus one ball from balls
    public void MinusOne() 
    {
        // Only function if the game has not been stopped
        if (Stop)
        {
            return;
        }

        // Notify observers of despawned ball
        if (NotifyDespawnBall != null)
        {
            NotifyDespawnBall();
        }

        // Change the text
        currentBalls -= 1;
        ballsLeftText.text = BallsLeftString;

        // Check condition to trigger gameEnd
        if (currentBalls <= 0) 
        {
            // Prevent currentBalls from going negative
            currentBalls = 0;

            // Notify observers
            if (NotifyGameEnd != null) {
                NotifyGameEnd();
            }
        }
    }

    // Generate a string with <balls_left/total_balls>, like "30/50"
    private string BallsLeftString { get { return currentBalls + "/" + ballsStart; } }
}
