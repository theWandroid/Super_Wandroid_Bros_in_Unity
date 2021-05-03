using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        //cuando el jugador entre dentro del trigger de la plataforma
        if(otherCollider.tag == "Player") 
        { 
            //enviara al animator un booleano llamado animStart con la señal tru para que empieze la animación
        animator.SetBool("animStart", true);
        }
    }
}
