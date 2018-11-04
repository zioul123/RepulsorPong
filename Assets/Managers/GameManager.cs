using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private Spawner[] spawners;
    [SerializeField]
    private Despawner[] despawners;
    [SerializeField]
    private int totalAllowableBalls;
    // Singleton
    public static GameManager gameManager;

	// Use this for initialization
	void Start() 
    {
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].spawnBall += () => TotalBalls += 1;
        }
        for (int i = 0; i < despawners.Length; i++)
        {
            despawners[i].despawnBall += () => TotalBalls -= 1;
        }
    }
	
    // Singleton pattern
    public static GameManager GetInstance() {
        if (gameManager == null) {
            gameManager = FindObjectOfType<GameManager>();
        }
        return gameManager;
    }

    // Number of balls spawned total
    public int TotalBalls { get; set; }
    // Whether balls can be spawned
    public bool IsSpawnAllowed { get { return TotalBalls < totalAllowableBalls; } }
}
