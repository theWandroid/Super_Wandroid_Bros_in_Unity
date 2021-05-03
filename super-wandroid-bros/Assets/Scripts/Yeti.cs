using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yeti : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    private Rigidbody2D _rigidBody;
    public Animator _animator;
    public bool turnAround;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (_rigidBody.velocity.x < runningSpeed)
            {
                if (turnAround == false)
                {
                    //this.transform.eulerAngles = new Vector3(0, 0, 0);
                    //el enemigo avanzara por defecto
                    _rigidBody.velocity = new Vector2(runningSpeed, _rigidBody.velocity.y);
                }

                if(turnAround == true)
                {
                    //this.transform.eulerAngles = new Vector3(0, 180.0f, 0);
                    _rigidBody.velocity = new Vector2(-runningSpeed, _rigidBody.velocity.y);

                }
            }
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("turn", turnAround);
    }
}
