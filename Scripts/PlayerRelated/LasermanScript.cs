using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasermanScript : MonoBehaviour
{
    public float lastShotTime;
    public GameObject enemyProjectilePrefab;
    public Rigidbody2D myRigidbody;

    private float xDirection;
    private SpriteRenderer sr;
    private float firstShotTime = -1; 
    public SpriteRenderer SpriteRenderer
    {
        get { return sr; }
    }

    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        if (sr.flipX == false)
        {
            xDirection = 1f;
        }
        else
        {
            xDirection = -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        LaserFire();

    }


    void LaserFire()
    {
        if (firstShotTime == -1)
        {
            firstShotTime = Time.time;
        }
        float timeActive = Time.time - firstShotTime;
        if (timeActive <= 3)
        {
            if (Time.time - lastShotTime > 0.1f)
            {
                Vector3 offset = new Vector3((xDirection > 0 ? -1 : 1), 0f, 0f);
                Vector3 spawnPos = transform.position + offset;
                lastShotTime = Time.time;
                GameObject projectile = Instantiate(enemyProjectilePrefab, spawnPos, Quaternion.identity);
                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
                float projectileSpeed = 10f * (xDirection > 0 ? -1 : 1);
                rb.velocity = new Vector2(projectileSpeed, 0f);
            }
        }
        else if (timeActive > 3 && timeActive <= 5)
        {
            return;
        }
        else
        {
            firstShotTime = -1;
        }
    }
}
