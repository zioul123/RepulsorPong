using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void NotifySpawnBall();
public class Spawner : MonoBehaviour
{
    // Position, velocity, and direction related
    // The ball to spawn
    [SerializeField]
    private readonly GameObject spawnPrefab;
    // The spawn point
    [SerializeField]
    private readonly GameObject spawnPoint;
    // The x-direction to shoot
    [SerializeField]
    private readonly float dirX = -1;
    // The y-direction to shoot
    [SerializeField]
    private readonly float dirY = 1;
    // The average speed of balls that spawn
    [SerializeField]
    private float speed = 200f;
    // The deviation range of the speed of balls spawned (+- around the average)
    [SerializeField]
    private float speedDev = 100f;
    // The rate that the average speed of balls spawned increases
    [SerializeField]
    private float speedIncreaseRate = 3f;

    // Timing related
    // The time before the first spawn
    [SerializeField]
    private float initialWait = 1f;
    // The average rate at which balls are spawned
    [SerializeField]
    private float spawnPerSecond = 0.25f;
    // The random range that balls can be spawned at (more than average)
    [SerializeField]
    private float deviation = 2f;
    // The rate at which spawn per second increases
    [SerializeField]
    private float increaseRate = 0.01f;
    // The object that the ball spawns as children under
    [SerializeField]
    private Transform ballParent;

    // Game stopper
    public bool Stop { get; set; }

    // Observer callback
    public event NotifySpawnBall NotifySpawnBall;

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

        // Only spawn while the game hasn't stopped
        while (!Stop) 
        {
            // Check if spawn is allowed
            if (GameManager.GetInstance().IsSpawnAllowed)
            {
                // Notify GameManager of spawned ball
                if (NotifySpawnBall != null) 
                {
                    NotifySpawnBall();
                }

                // Spawn and set the velocity of the ball
                GameObject spawnedBall = Instantiate(spawnPrefab, spawnPoint.transform.position, transform.rotation, ballParent);
                spawnedBall.GetComponent<Ball>().Init(new Vector2(dirX + Random.Range(-1f, 1f), dirY + Random.Range(-1f, 1f)),
                                                      speed + Random.Range(-speedDev, speedDev));
            }

            // Wait before spawning next ball
            yield return new WaitForSeconds(1 / spawnPerSecond + Random.Range(0f, deviation));
        }
    }
}
