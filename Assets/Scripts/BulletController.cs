using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Class variables
    public float speed = 10.0f;
    public float bulletStrength = 30.0f;

    private float xBoundary = 40;
    private float zBoundary = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);

        // If projectile exceeds boundaries then destroy the projectile
        if(transform.position.x > xBoundary || transform.position.x < -xBoundary || transform.position.z > zBoundary || transform.position.z < -zBoundary)
        {
            Destroy(gameObject);
        }
    }


}
