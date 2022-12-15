using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMvmt movement;
    public Collider boxCollider;
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().EndGame();
        }

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            boxCollider.enabled = !boxCollider.enabled;
        }*/

        
        /*(gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider).enabled = true;
        {
            if (Input.GetKey("g"))
            {
                (gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider).enabled = false;
            }

            if (Input.GetKey("n"))
            {
                (gameObject.GetComponent(typeof(BoxCollider)) as BoxCollider).enabled = true;
            }
        }*/
    }
}
