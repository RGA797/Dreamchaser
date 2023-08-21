using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float speed = 1f;
    public float length = 5f;
    public bool isHorizontal = false;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

        float pos = Mathf.PingPong(Time.time * speed, Mathf.Abs(length));
        Vector3 direction = isHorizontal ? Vector3.right : Vector3.up;
        Vector3 newPosition = startPosition + direction * pos * Mathf.Sign(length);
        transform.position = newPosition;
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