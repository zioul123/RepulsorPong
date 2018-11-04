﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void DespawnBall();
public class Despawner : MonoBehaviour 
{
    [SerializeField]
    private Text ballsLeftText;
    // Initial number of balls
    [SerializeField]
    private int ballsStart = 30;
    // Used to track number of balls left
    private int currentBalls;

    public event DespawnBall despawnBall;

    // Use this for initialization
    void Start() 
    {
        ballsLeftText.text = ballsStart + "/" + ballsStart;
        currentBalls = ballsStart;
	}
	
    // Minus one ball from balls
    public void MinusOne() 
    {
        Debug.Log("Minus one");

        // Notify GameManager
        if (despawnBall != null) {
            despawnBall();
        }

        // Change the text
        currentBalls -= 1;
        ballsLeftText.text = BallsLeftString;
    }

    // Generate a string like "30/50"
    private string BallsLeftString { get { return currentBalls + "/" + ballsStart; } }
}
