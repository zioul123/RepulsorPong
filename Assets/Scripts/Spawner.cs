using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
    // Position, velocity, and direction related
    [SerializeField]
    GameObject spawnPrefab;
    [SerializeField]
    GameObject spawner;
    [SerializeField]
    private float dirX = -1;
    [SerializeField]
    private float dirY = 1;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float speedDev = 1;
    [SerializeField]
    private float speedIncreaseRate = 0.1f;

    // Timing related
    [SerializeField]
    private float initialWait = 1f;
    [SerializeField]
    private float spawnPerSecond = 0.25f;
    [SerializeField]
    private float deviation = 2f;
    [SerializeField]
    private float increaseRate = 0.01f;
    [SerializeField]
    private Transform ballParent;

    // Game stopper
    private bool stop;

    // Use this for initialization
    void Start () {
        StartCoroutine(Spawn());
	}
	
	void FixedUpdate () {
        // Increase spawn rate
        spawnPerSecond += increaseRate * Time.deltaTime;
        // Increase speed
        speed += speedIncreaseRate * Time.deltaTime;
	}

    // Spawn after a delay, then continuously spawn balls until stop is true.
    private IEnumerator Spawn() 
    {
        yield return new WaitForSeconds(initialWait);

        while (!stop) {
            // Spawn and set the velocity of the ball
            GameObject spawnedBall = Instantiate(spawnPrefab, spawner.transform.position, transform.rotation, ballParent);
            spawnedBall.GetComponent<Ball>().Init(new Vector2(dirX + Random.Range(-1f, 1f), dirY + Random.Range(-1f, 1f)),
                                                  speed + Random.Range(-speedDev, speedDev)); 

            // Wait before spawning next ball
            yield return new WaitForSeconds(1 / spawnPerSecond + Random.Range(0f, deviation));
        }
    }
}
