using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    public float spawnRange = 9;
    private int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemies(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
            waveNumber += 1;
        }
    }

    private Vector3 GenerateSpawnPosition ()
    {
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPositionX, 0, spawnPositionZ);
    }

    private void SpawnEnemies(int numberOfEnemiesToSpawn)
    {
        for (int i=1; i<=numberOfEnemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
