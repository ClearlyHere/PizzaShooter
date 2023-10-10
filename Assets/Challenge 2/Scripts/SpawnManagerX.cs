using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22;
    private float spawnLimitXRight = 7;
    private float spawnPosY = 30;

    private float startTime = 0f;
    private float spawnMinimumInterval = 3f;
    private float spawnMaximumInterval = 6f;
    public float randomTime = 0;

    // Start is called before the first frame update
    void Update()
    {
        SpawnRandomBall();
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        if (startTime >= randomTime)
        {
            // Generate random ball index and random spawn position
            Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
            int ballIndex = Random.Range(0, ballPrefabs.Length);
            // instantiate ball at random spawn location
            Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
            startTime = 0.0f;
            randomTime = Random.Range(spawnMinimumInterval, spawnMaximumInterval);
        }
        startTime += Time.deltaTime;
    }
}
