using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Class variables
    public float speed = 3.0f;
    public bool isBoss = false;
    public float spawnInterval;
    public int miniEnemySpawnCount;

    private Rigidbody enemyRb;
    private GameObject player;
    private float nextSpawn;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (isBoss)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        // Destroy object if it falls off thge island
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the enemy is hit by the bullet then launch the enemy back and destroy the bullet
        if (other.CompareTag("Projectile"))
        {
            float bulletStrength = other.GetComponent < BulletController >().bulletStrength;
            Destroy(other.gameObject);

            Vector3 oppositeOfBullet = transform.position - other.gameObject.transform.position;
            enemyRb.AddForce(oppositeOfBullet * bulletStrength, ForceMode.Impulse);

        }
    }
}
