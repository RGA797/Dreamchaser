using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyProjectileScript : MonoBehaviour
{
    public float maxTravelDistance;
    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float travelledDistance = Vector3.Distance(transform.position, startPosition);
        if (travelledDistance >= maxTravelDistance)
        {
            Destroy(gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(other.gameObject); // destroy the enemy game object
            Destroy(gameObject); // destroy the projectile game object
        }
    }

}
