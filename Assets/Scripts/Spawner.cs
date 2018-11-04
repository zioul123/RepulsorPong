using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SpawnBall();
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
    private float speed = 200f;
    [SerializeField]
    private float speedDev = 100f;
    [SerializeField]
    private float speedIncreaseRate = 3f;

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
    public bool Stop { get; set; }

    public event SpawnBall spawnBall;

    // Use this for initialization
    void Start () {
        Stop = false;
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

        while (!Stop) {
            // Check if spawn is allowed
            if (GameManager.GetInstance().IsSpawnAllowed) {
                // Notify GameManager of spawned ball
                if (spawnBall != null) {
                    spawnBall();
                }
                // Spawn and set the velocity of the ball
                GameObject spawnedBall = Instantiate(spawnPrefab, spawner.transform.position, transform.rotation, ballParent);
                spawnedBall.GetComponent<Ball>().Init(new Vector2(dirX + Random.Range(-1f, 1f), dirY + Random.Range(-1f, 1f)),
                                                      speed + Random.Range(-speedDev, speedDev));

            }


            // Wait before spawning next ball
            yield return new WaitForSeconds(1 / spawnPerSecond + Random.Range(0f, deviation));
        }
    }


}
