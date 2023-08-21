using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closingPlatformScript : MonoBehaviour
{
    public float speed = 1f;
    public float length = 5f;
    public bool isHorizontal = false;
    public GameObject player;

    private bool triggerHasMoved = false;


    private Vector3 startPosition;
    private Vector3 currentPosition;
    private Vector3 direction;
    private Vector3 playerStartPosition;


    private float moveTime;

    private float pos;
    void Start()
    {
        startPosition = transform.position;
        playerStartPosition = player.transform.position;
    }

    void Update()
    {

        startPosition = transform.position;
        if (!triggerHasMoved)
        {
            if (player.transform.position[0] != playerStartPosition[0])
            {
                triggerHasMoved = true;
                currentPosition = startPosition;
                moveTime = Time.time;
            }
        }

        if (triggerHasMoved)
        {
            pos = Mathf.PingPong((moveTime - Time.time) * speed, Mathf.Abs(length));
            Vector3 direction = isHorizontal ? Vector3.right : Vector3.up;
            Vector3 newPosition = currentPosition + direction * pos * Mathf.Sign(length);
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