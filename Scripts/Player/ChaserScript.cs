using System;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class ChaserScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public GameObject friendlyProjectilePrefab;
    private float jumpingSpeed = 7f;
    private float walkingSpeed = 4f;
    private float fireRate = 0.5f;
    private int maxJumps = 0;
    public GameStateScript gameState;

    private bool isCrouching = false;
 
    private float dashCooldown = 1f; // the time in seconds between dashes
    private float dashDuration = 0.1f; // the duration of the dash in seconds
    private float lastDashTime = 0f;

    private float xDirection = 1f;
    private float lastShotTime;
    private int jumpCounter;
    private bool canShoot = false;
    private bool canCrouch = false;
    private bool canDash = false;
    private bool isDashing = false; 

    private SpriteRenderer sr;
    private GameManagerScript gameManager;
    

    // Property to get the sprite renderer. used in testing
    public SpriteRenderer SpriteRenderer
    {
        get { return sr; }
    }

    // Start is called before the first frame update
    void Start()
    {
       
        xDirection = 1f;
        myRigidbody.drag = 0;
        sr = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManagerScript>();

        //jumping unlocks lv. 1, 3, 7
        if (gameState.currentLevel > 0) { 
            if (gameState.currentLevel >= 1 && gameState.currentLevel < 3)
            {
                maxJumps = 1;
            }
            else if (gameState.currentLevel > 2 && gameState.currentLevel < 7)
            {
                maxJumps = 2;
            }
            else if (gameState.currentLevel > 6)
            {
                maxJumps = 3;
            }
        }


        //crouching. unlocks lv. 4
        if (gameState.currentLevel > 3)
        {
            canCrouch = true;
        }


        //shooting unlock lv 2
        if (gameState.currentLevel > 1)
        {
            canShoot = true;
        }

        //dashing unlock lv 5.
        if (gameState.currentLevel > 4)
        {
            canDash = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!gameState.isPaused)
        {
            // controls 

            //walking
            xDirection = Input.GetAxis("Horizontal");
            if (Math.Abs(xDirection) > 0 && !isCrouching)
            {
                Move();
            }
            else
            {
                myRigidbody.velocity = new Vector2(0, myRigidbody.velocity.y);
            }

            //jumping. up by lv 1
            if (Input.GetButtonDown("Jump") && jumpCounter < maxJumps)
            {
                Jump();
            }

            //shooting
            if (Input.GetButtonDown("Fire1") && Time.time - lastShotTime > fireRate && canShoot && !isCrouching)
            {
                Shoot();
            }

            //crouching
            if (Input.GetButton("Crouch") && canCrouch)
            {
                if (!isCrouching)
                {
                    Crouch();
                }
            }

            if (Input.GetButtonUp("Crouch"))
            {
                if (isCrouching)
                {
                    Stand();
                }
            }

            if (isDashing)
            {
                myRigidbody.velocity = new Vector2(xDirection * walkingSpeed * 5, myRigidbody.velocity.y);
                if(Time.time - lastDashTime > dashDuration)
                {
                    isDashing = false;
                }
            }

            if (Input.GetButton("Dash") && canDash && Time.time - lastDashTime > dashCooldown && !isDashing && !isCrouching)
            {
                Dash();
            }
        }
    }

    //spawns a projectile and shoots it with a set velocity
    private void Shoot()
    {
        Vector3 spawnPos = transform.position + new Vector3((sr.flipX ? -1 : 1), 0f, -0.5f);
        lastShotTime = Time.time;
        GameObject projectile = Instantiate(friendlyProjectilePrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        float projectileSpeed = 10f;
        rb.velocity = new Vector2(projectileSpeed * (sr.flipX ? -1 : 1), 0f);
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

    //jumps, and counts up the jump counter
    private void Jump()
    {
        jumpCounter++;
        myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpingSpeed);
    }

    private void Dash()
    {
        lastDashTime = Time.time;
        isDashing = true;
    }

    private void Crouch()
    {
        isCrouching = true;
        transform.Rotate(0f, 0f, sr.flipX ?  107.164f: -72.836f);
    }

    private void Stand()
    {
        isCrouching = false;
        transform.Rotate(0f, 0f, sr.flipX ? -107.164f : 72.836f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            jumpCounter = 0;
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            gameManager.PastLevel();
        }
    }
}