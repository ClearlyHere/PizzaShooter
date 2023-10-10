using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float spawnTime = 0f;
    private float spawnDelay = 1f;

    // Update is called once per frame
    void Update()
    {
        spawnDog();
    }

    void spawnDog()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && spawnTime >= spawnDelay)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            spawnTime = 0;
        }
        spawnTime += Time.deltaTime;
    }
}
