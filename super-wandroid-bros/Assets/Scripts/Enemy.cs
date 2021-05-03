using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;


    private Rigidbody2D _rigidBody;

    public static bool turnAround;
    private Transform enemyStartPosition;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        enemyStartPosition = this.transform;
    }

    private void Start()
    {
        this.transform.position = enemyStartPosition.position;

    }

    void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        //si es true entonces girara y se movera hacia la izquierda, porque la velocidad es negativa
        if(turnAround == true)
        {
            //aqui la velocidad es positiva...
            currentRunningSpeed = -runningSpeed;

            this.transform.eulerAngles = new Vector3(0, 180.0f, 0);
        }
        else
        {
            //en cambio si recibe que es false ira para la derecha, ya que la velocidad es poositiva y el angulo de rotacion será el mismo de la imagen que hemos creado
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }




        //el enemigo solo se movera si estamos en modo de juego
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
   
                _rigidBody.velocity = new Vector2(currentRunningSpeed, _rigidBody.velocity.y);
            
            Debug.Log(_rigidBody.velocity.x);
        }
    }

}