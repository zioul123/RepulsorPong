using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DespawnBall();
public delegate void GameEnd();
public class Despawner : MonoBehaviour 
{
    [SerializeField]
    private Text ballsLeftText;
    // Initial number of balls
    [SerializeField]
    private int ballsStart = 30;
    // Used to track number of balls left
    private int currentBalls;
    // Stop the despawner from subtracting scores when game ends
    public bool Stop { get; set; }

    public event DespawnBall despawnBall;
    public event GameEnd gameEnd;

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
        if (!Stop) {
            Debug.Log("Minus one");

            // Notify GameManager
            if (despawnBall != null)
            {
                despawnBall();
            }

            // Change the text
            currentBalls -= 1;
            ballsLeftText.text = BallsLeftString;

            if (currentBalls <= 0) {
                currentBalls = 0;
                if (gameEnd != null) {
                    gameEnd();
                }
            }
        }
    }

    // Generate a string like "30/50"
    private string BallsLeftString { get { return currentBalls + "/" + ballsStart; } }
}
