using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    // Spawners on the field
    [SerializeField]
    private Spawner[] spawners;
    // Despawners behind players
    [SerializeField]
    private Despawner[] despawners;
    // The object that all balls will be spawned as children of
    [SerializeField]
    private GameObject ballParent;
    // The limit of number of balls on the field
    [SerializeField]
    private int totalAllowableBalls;
    // The text field that announces the winner
    [SerializeField]
    private Text winnerText;

    // Singleton
    public static GameManager gameManager;

	// Use this for initialization
	void Start() 
    {
        // Add callbacks to spawners
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].NotifySpawnBall += () => TotalBalls += 1;
        }

        // Add callbacks to despawners
        for (int i = 0; i < despawners.Length; i++)
        {
            int iCopy = i; // Used to capture the variable in the gameEnd callback
            despawners[i].NotifyDespawnBall += () => TotalBalls -= 1;
            despawners[i].NotifyGameEnd += () => EndGame(iCopy);
        }
    }

    // Trigger game lose for the specified player
    private void EndGame(int loser)
    {
        Debug.Log("Loser: " + loser);

        // Stop spawners from functioning
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].Stop = true;
        }

        // Stop despawners from functioning
        for (int i = 0; i < despawners.Length; i++)
        {
            despawners[i].Stop = true;
        }

        // Destroy balls after a delay
        StartCoroutine(DestroyAllBalls());

        // Display winner text
        int winner = loser == 0 ? 1 : 0;
        DisplayWinner(winner);
    }

    // Destroy all balls after a delay
    private IEnumerator DestroyAllBalls() 
    {
        yield return new WaitForSeconds(5);

        foreach (Transform child in ballParent.transform) 
        {
            Destroy(child.gameObject);
        }
    }

    // Display the text to indicate the winner
    private void DisplayWinner(int winner)
    {
        string winnerString = winner == 1 ? "Blue" : "Red";
        winnerText.text = "Winner is " + winnerString + " player!";
        winnerText.color = winner == 1 ? Color.blue : Color.red;
        winnerText.enabled = true;
    }

    // Singleton pattern to get GameManager
    public static GameManager GetInstance() 
    {
        if (gameManager == null) 
        {
            gameManager = FindObjectOfType<GameManager>();
        }
        return gameManager;
    }

    // Number of balls spawned total
    public int TotalBalls { get; set; }
    // Whether balls can be spawned
    public bool IsSpawnAllowed { get { return TotalBalls < totalAllowableBalls; } }
}
