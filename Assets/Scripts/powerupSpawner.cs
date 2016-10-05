using UnityEngine;
using System.Collections;

public class powerupSpawner : MonoBehaviour {
    public GameObject[] powerUps;
    public GameObject spawner;
    public float spawnCounter;
	// Use this for initialization
	void Start () {
    spawnCounter = 0;

    }
	
	// Update is called once per frame
	void Update () {
        spawnCounter += Time.deltaTime;

        if (spawnCounter > 2)
        {
			Instantiate(powerUps[Random.Range(0,powerUps.Length)], new Vector3(Random.Range(0, 450), 20, Random.Range(0, 450)), spawner.transform.rotation);
            spawnCounter = 0;
        }

    }
}
