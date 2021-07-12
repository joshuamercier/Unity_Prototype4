using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Class variables
    public  GameObject [] enemyPrefabs;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject [] powerupPrefabs;

    private float spawnRange = 9;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;

        //If all enemies destroyed spawn the next wave
        if(enemyCount == 0)
        {
            // Increase the wave
            waveNumber++;
            // Spawn the waves enemies
            SpawnEnemyWave(waveNumber);
            // Spawn another powerup
            SpawnPowerup();
        }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for(int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomIndex], GenerateSpawnPosition(), enemyPrefabs[randomIndex].transform.rotation);
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        // Determine random position of X and Z coordiantes for enemy
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        // Assign the coordiantes to a Vector3
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    private void SpawnPowerup()
    {
        int randomIndex = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomIndex], GenerateSpawnPosition(), powerupPrefabs[randomIndex].transform.rotation);
    }
}
