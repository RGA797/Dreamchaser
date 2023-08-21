using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingWallScript : MonoBehaviour
{
    public float speed = 1f;
    public float length = 5f;
    public bool isHorizontal = false;
    public GameObject player;


    private bool playerHasMoved = false;


    private Vector3 startPosition;
    private Vector3 playerStartPosition;

    void Start()
    {
        startPosition = transform.position;
        playerStartPosition = transform.position;
    }

    void Update()
    {
        if(player.transform.position != playerStartPosition)
        {
            playerHasMoved = true;
        }

        if (playerHasMoved)
        {
            float pos = Mathf.PingPong(Time.time * speed, Mathf.Abs(length));
            Vector3 direction = isHorizontal ? Vector3.right : Vector3.up;
            Vector3 newPosition = startPosition + direction * pos * Mathf.Sign(length);
            transform.position = newPosition;
        }
    }

    private Vector3 originalScale;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            originalScale = other.gameObject.transform.localScale;
            Transform passenger = other.gameObject.transform;
            passenger.SetParent(transform);
            passenger.localScale = originalScale;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Transform passenger = other.gameObject.transform;
            if (passenger.parent == transform)
            {
                passenger.SetParent(null);
                passenger.localScale = originalScale;
            }
        }
    }
}