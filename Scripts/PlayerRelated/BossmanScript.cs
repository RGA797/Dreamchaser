using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossmanScript : MonoBehaviour
{
    public float lastShotTime;
    public GameObject enemyProjectilePrefab;
    private float walkingSpeed = 4f;
    public Rigidbody2D myRigidbody;

    private float xDirection;
    private SpriteRenderer sr;
    public int firerate = 1;
    public int startBuffer = 0;
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
        RapidFire();
    
    }

    void RapidFire()
    {
        if (Time.time - lastShotTime > firerate && Time.time - lastShotTime > startBuffer)
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

    //moves in the direction of xDirection. flips the sprite model as needed
    public void Move()
    {
        myRigidbody.velocity = new Vector2(xDirection * walkingSpeed, myRigidbody.velocity.y);
        if (xDirection > 0)
        {
            sr.flipX = false;
        }
        else if (xDirection < 0)
        {
            sr.flipX = true;
        }
    }

}
