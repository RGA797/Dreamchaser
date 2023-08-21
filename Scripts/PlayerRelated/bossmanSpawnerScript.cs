using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossmanSpawnerScript : MonoBehaviour
{
    public GameObject bossman;
    private SpriteRenderer sr;
    private float lastSpawnTime = 0;
    private float spawnCounter = 0;
    private int maxBossmen = 1;


    // Property to get the sprite renderer. used in testing
    public SpriteRenderer SpriteRenderer
    {
        get { return sr; }
    }
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        lastSpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-lastSpawnTime > 8f)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 spawnPos = transform.position + new Vector3((sr.flipX ? -2f : 2f), -0.5f, 0f);
        lastSpawnTime = Time.time;
        GameObject bossMan = Instantiate(bossman, spawnPos, Quaternion.identity);
        Rigidbody2D rb = bossMan.GetComponent<Rigidbody2D>();
        bossman.GetComponent<BossmanScript>().startBuffer = 3;
        bossman.GetComponent<BossmanScript>().firerate = 2;
        spawnCounter++;

        if(spawnCounter == maxBossmen)
        {
            Destroy(this.gameObject);
        }

    }


}
