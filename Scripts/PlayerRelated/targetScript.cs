using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


//player related object. has custom ui responsibility
//counts the number of collisions it has with player
//changes to random positions and updates its own ui on touch
public class targetScript : MonoBehaviour
{
    public GameObject targetPrefab;
    public GameObject dreamBox;
    private int targetsHit = 0;
    [SerializeField] private TextMeshProUGUI targetsHitText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("friendlyProjectile")))
        {
           

            //updateText and counter
            targetsHit++;
            targetsHitText.text = targetsHit.ToString() + "/10";

            if (targetsHit < 10)
            {
                Vector3 position = transform.position;

                //go to the opposite x position
                float xPosition = Random.Range(position.x < 0 ? 1f : -7f, position.x < 0 ? 7f : -1f);
                position.x = xPosition;

                //go to a random step
                float[] yPositions = { 0.4f, 2.6f, 4.7f };
                float yPosition = yPositions[Random.Range(0, yPositions.Length)];
                position.y = yPosition;

                // updating position
                transform.position = position;
            }

            else if(targetsHit == 10)
            {     
                Destroy(dreamBox);
                Destroy(gameObject);
           
            }

        }
    }
}
