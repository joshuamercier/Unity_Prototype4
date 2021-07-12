using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Class variables
    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject projectile;

    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        // Update the powerup indicator so it is arround the player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            if(other.name == "Powerup")
            {
                hasPowerup = true;
                Destroy(other.gameObject);
                // Show indicator
                powerupIndicator.gameObject.SetActive(true);
                // Start countdown
                StartCoroutine(PowerupCountdownRoutine());
            }
            else if(other.name == "Powerup_Bullets")
            {
                // Grab current position of player
                Vector3 thisPos = transform.position;
                // Instantiate the four bullets
                GameObject bullet1 = Instantiate(projectile, thisPos, projectile.transform.rotation);
                GameObject bullet2 = Instantiate(projectile, thisPos, projectile.transform.rotation);
                GameObject bullet3 = Instantiate(projectile, thisPos, projectile.transform.rotation);
                GameObject bullet4 = Instantiate(projectile, thisPos, projectile.transform.rotation);
                // Change their roations
                bullet1.transform.Rotate(0f, 0f, 0f);
                bullet2.transform.Rotate(0f, 90f, 0f);
                bullet3.transform.Rotate(0f, 180f, 0f);
                bullet4.transform.Rotate(0f, 270f, 0f);
                // Destroy the powerup as it is a once use
                Destroy(other.gameObject);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position;
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Debug.Log("Collieded with " + collision.gameObject.name + " with powerup set to " + hasPowerup);
            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        // Hide indicator
        powerupIndicator.gameObject.SetActive(false);
    }
}
