using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    //public bool movingForward;
    //public bool choke;


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
       /* if(otherCollider.tag == "Collectable" || otherCollider.tag == "Player")
        {
            return;
        }
        
        if (otherCollider.tag == "Flower")
        {


            if (movingForward == true)
            {
                Enemy.turnAround = true;
            }
            else
            {
                Enemy.turnAround = false;
            }
        }
        movingForward = !movingForward;
        */

        //cuado toque un GameObject con collider canviara de valor un booleano a false, que le dira a la clase enemy que ha de girar hacia la derecha
        if(otherCollider.name == "Flower Left")
        {
            Enemy.turnAround = false;

        }

        //cuado toque un GameObject con collider canviara de valor un booleano a true, que le dira a la clase enemy que ha de girar hacia la izquierda
        if (otherCollider.name == "Flower Right")
        {
            Enemy.turnAround = true;

        }

    }



}
