using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //paara hacer un singleton lo unico que tenemos que hacer es instanciarlo como estatico e inicializarlo en el awake
    public static PlayerController sharedInstance;

    public float jumpForce = 5.0f;
    public float runningSpeed = 1.5f;
    private Rigidbody2D _rigidBody;
    public Animator _animator;
    private Vector3 startPosition;
    private int healthPoints;
    private int manaPoints;
    public float currentSpeed;

    public const int INITIAL_HEALTH= 100;
    public const int MAX_HEALTH = 150;
    public const int MIN_HEALTH = 10;
    public const int INITIAL_MANA = 15;
    public const int MAX_MANA = 25;
    public const int MIN_MANA = 0;
    public const float MIN_SPEED = 2.5f;
    public const float HEALTH_TIME_DECREASE = 1.0f;
    public const float SUPERJUMP_FORCE = 1.5f ;
    public const int SUPERJUMP_COST = 3;




    private void Awake()
    {
        sharedInstance = this;
        _rigidBody = GetComponent<Rigidbody2D>();
        //la variable startposition toma el valor de donde empieza la primera vez nuestro personaje
        startPosition = this.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //solo saltara si esta en modo de juego según el gameManager
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        { //saltar es mejor que vaya en el update, ya que es una cosa que depende de muchas mas
            //el personaje saltara cuando pulsemos el espacio o el raton
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Jump(false) ;
            }

            if ( Input.GetMouseButtonDown(1))
            {
                Jump(true);
            }
        }
    }

    public void StartGame() {
        _animator.SetBool("isAlive", true);
        _animator.SetBool("isGrounded", true);
        //cada vez que reinicia el personaje lo ponemos en la posición de inicio
        //this.transform.position = startPosition;
        //this.transform.position = this.startPosition;

        this.transform.position = LevelGenerator.sharedInstance.currentBlocks[0].startPoint.position;
         GameManager.sharedInstance.BackToMenu();
        this.healthPoints = INITIAL_HEALTH;
        Debug.LogFormat($"Tengo {this.healthPoints} puntos de salud.");
        this.manaPoints = INITIAL_MANA;
        Debug.LogFormat($"Tengo {this.manaPoints} puntos de mana.");
        //arranco la corrutina
        //StartCoroutine("TirePlayer");
    }

    //vamos a declarar una corutina
    //las corutinas son metodos que se llaman a si mismos
    //El enumarator es una funcion que corre por frames
    /*
    IEnumerator TirePlayer()
    {
       
            //mientas haya puntos de vida
            while (this.healthPoints > MIN_HEALTH)
            {
                this.healthPoints--;
                //le decimos que espere UN segundo antes de volverse a ejecutar
                yield return new WaitForSeconds(HEALTH_TIME_DECREASE);
            }
        
        //si no tiene puntos de vida que se salga, la para durante un frame
        yield return null;
    }
    */
    //es perfecto para aplicar fuerzas constantes, para movimientos fijos
    void FixedUpdate()

    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //currentSpeed = (runningSpeed -0.5f) * (float)this.healthPoints / 100;

            currentSpeed = runningSpeed;
            if(_rigidBody.velocity.x < currentSpeed) {
            //el`personaje avanzara por defecto
            _rigidBody.velocity = new Vector2(currentSpeed, _rigidBody.velocity.y);
            }
            Debug.LogFormat("La velocidad del personaje es de " +_rigidBody.velocity.x);
        }

       
        /*
        if (Input.GetKeyDown(KeyCode.D)) { 
        //todo lo que tenga que ver con la fisica estará ligado al rigidBody
        //si la velocidad del personake es menor que la velocidad maxima que hemos definido, servira en el caso de que este parado
        if (_rigidBody.velocity.x < runningSpeed)
        {
            //entonces la velocidad del personaje sera igual a la velocidad maxima en el eje de las x y la velocidad del personaje en el eje de las y
            _rigidBody.velocity = new Vector2(runningSpeed, _rigidBody.velocity.y);
        }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //todo lo que tenga que ver con la fisica estará ligado al rigidBody
            //si la velocidad del personake es menor que la velocidad maxima que hemos definido, servira en el caso de que este parado
            if (_rigidBody.velocity.x > -runningSpeed)
            {
                //entonces la velocidad del personaje sera igual a la velocidad maxima en el eje de las x y la velocidad del personaje en el eje de las y
                _rigidBody.velocity = new Vector2(-runningSpeed, _rigidBody.velocity.y);
            }
        }
        */
    }

    void LateUpdate()
    {
        //le estamos enviado al animator la variable isGrounded true o false  segun si saltamos o no mediante el metodo que hicimos 
        _animator.SetBool("isGrounded", IsTouchingTheGround());

    }

    void Jump( bool isSuperJump)
    {
        //F = m*a ==>
        //multiplicamos una fueza vertcal pro la variable jumpForce que hemos creadoç
        //ponemos impulso porque aplicamos fuerza en un instante
        if(IsTouchingTheGround()) {
            //Debug.Log("Estoy saltando");
            //con mana se saltara mas alto
            if(isSuperJump && this.manaPoints >= SUPERJUMP_COST)
            {
                manaPoints -= SUPERJUMP_COST;
                Debug.LogFormat($"Tengo {this.manaPoints} puntos de mana.");
                _rigidBody.AddForce(Vector2.up * jumpForce *SUPERJUMP_FORCE, ForceMode2D.Impulse);
            }
            else 
            { 
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
    //esta variable sirve para detectar la capa del suelo
    public LayerMask groundLayer;
    bool IsTouchingTheGround()
    {
        //si al trazar un rayo empezando desde el personaje y mirando hacia abajo hasta un maximo de 20 cm y nos encontramos contra el suelo, entonces nos devolvera true si se cumple
        //se lanza un rayo del personaje hacia abajo hasta un maximo de 20 cm y si encuentra un elemento devuelve true
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.1f, groundLayer))
        {
            //Debug.Log("Estoy tocando el suelo");

            return true;

        }
        else
        {
            //Debug.Log("No puedo saltar porque no toco el suelo");

            return false;
        }
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        //le comunicamos al animator que la variable isAlive debe de ser falsa
        this._animator.SetBool("isAlive", false);
        float currentMaxScore = PlayerPrefs.GetFloat("maxScore", 0);

        if (currentMaxScore < this.GetDistance())
        {
            PlayerPrefs.SetFloat("maxScore", this.GetDistance());
        }

        //la paramos, porque si no se nos van añadiendo corutinas cada vez que el jugador renace
        StopCoroutine("TirePlayer");
    }

    public float GetDistance()
    {
        float travelledDistance = Vector2.Distance(new Vector2(startPosition.x, 0), new Vector2(this.transform.position.x, 0));
        return travelledDistance;
    }

    public void CollectHealth(int points)
    {
        this.healthPoints += points;
        //si el maximo de salut es mayor o igual al mana maximo establecido el valor de salut se quedara en el maximo, no subira mas

        if (this.healthPoints >= MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        //si el maximo de mana es mayor o igual al mana maximo establecido el valor de mana se quedara en el maximo, no subira mas
        if(this.manaPoints >= MAX_MANA)
        {
            manaPoints = MAX_MANA;
        }
    }
    //devolvera la vida del jugador
    public int GetHealth()
    {
        return this.healthPoints;
    }

    //devolvera los puntos del jugador
    public int GetMana()
    {
        return this.manaPoints;
    }


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Enemy")
        {
            this.healthPoints -= 10;
            
        }

        if(otherCollider.tag == "Flower")
        {
            this.healthPoints -= 5;
        }

        //solo mataremos al personaje si estea en modo juego y no tiene vida
        if ( GameManager.sharedInstance.currentGameState == GameState.inGame && this.healthPoints <= 0)
        {

            Kill();
        }
    }

}
