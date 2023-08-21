using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//hostile enemy object
public class SpikeScript : MonoBehaviour
{
    private GameManagerScript gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
