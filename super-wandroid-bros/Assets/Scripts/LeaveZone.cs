using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveZone : MonoBehaviour
{
    float timeSinceLastDestruction = 0.0f;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //solo se podrá volver a destruir si el tiempo desde la ultima destrucción es superior a 3 segundos
        if(collision.gameObject.tag == "Player" && timeSinceLastDestruction > 3.0f) { 
        LevelGenerator.sharedInstance.AdddLevelBlocK();
        LevelGenerator.sharedInstance.RemoveOldestBlock();
        timeSinceLastDestruction = 0.0f;
        }
      
      
    }

    private void Update()
    {
        timeSinceLastDestruction += Time.deltaTime;
    }
}
