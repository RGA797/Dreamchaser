using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamScript : MonoBehaviour
{
    public GameManagerScript gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //upon player touch, go to next level
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.NextLevel();
        }
    }
}
