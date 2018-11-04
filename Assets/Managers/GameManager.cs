using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    [SerializeField]
    private Spawner[] spawners;
    [SerializeField]
    private Despawner[] despawners;
    [SerializeField]
    private GameObject ballParent;
    [SerializeField]
    private int totalAllowableBalls;
    [SerializeField]
    private Text winnerText;

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
            int iCopy = i;
            despawners[i].despawnBall += () => TotalBalls -= 1;
            despawners[i].gameEnd += () => EndGame(iCopy);
        }
    }

    // Trigger game lose for the specified player
    private void EndGame(int loser) {
        Debug.Log("Loser: " + loser);
        // Stop spawner and despawner functions
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Stop = true;
        }
        for (int i = 0; i < despawners.Length; i++)
        {
            despawners[i].Stop = true;
        }

        // Destroy balls after a delay
        StartCoroutine(DestroyAllBalls());

        // Display winner text
        string winner;
        winner = loser == 0 ? "Blue" : "Red";
        winnerText.text = "Winner is " + winner + " player!";
        winnerText.color = loser == 0 ? Color.blue : Color.red;
        winnerText.enabled = true;
    }

    private IEnumerator DestroyAllBalls() 
    {
        yield return new WaitForSeconds(5);

        foreach (Transform child in ballParent.transform) 
        {
            Destroy(child.gameObject);
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
