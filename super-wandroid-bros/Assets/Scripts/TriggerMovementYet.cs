using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    right,
    left
}
public class TriggerMovementYeti : MonoBehaviour
{
    public bool movingForward;
    private Yeti enemyBehaviour;
    public TriggerType type;

    private void Start()
    {
        enemyBehaviour = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Yeti>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Enemy")
        {
            switch(type)
            {
                case TriggerType.right:
                    enemyBehaviour.turnAround = true;
                    break;

                case TriggerType.left:
                    enemyBehaviour.turnAround = false;
                    break;
            }
        }

       // movingForward = !movingForward;
    }

}
