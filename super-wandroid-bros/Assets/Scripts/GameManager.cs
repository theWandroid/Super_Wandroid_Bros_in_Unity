using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//EL ENUMERADO ES UNA CLASE QUE SOLO PUEDE TOMAR UNA serie de valores, es una serie de valores, constantes que podemos hacer servir para definir un estado, solo puede tomar un conjunto de valores, que los definimos al princìpio
// vamos a definir los posibles estados del videojuego
// son los posibles estados en que estara el juego


public enum GameState
{
    menu,
    inGame,
    gameOver,
    pause
}


/// <summary>
/// Singleton
/// </summary>
/// Lo que hace un singleton basicamente es restringir la instanciacion de una determinada classe, lo que se puede hacer son diferentes referencias a una misma instancia
/// 
public class GameManager : MonoBehaviour
{
    //el propio gameManager se creara a si mismo, la propia clase se crea a si misma

    //variable que referencia al propio GameManager
    //habrá un unico manager compartido por todo el mundo
    //paara hacer un singleton lo unico que tenemos que hacer es instanciarlo como estatico e inicializarlo en el awake

    public static GameManager sharedInstance;
    public GameObject menuCanvas, gameCanvas, gameOverCanvas, gamePause;
    public int collectedObjects;
    public Text coinText;
    private void Awake()
    {
        sharedInstance = this;
    }


    //variable para saber en que estado estamos en el momento actual
    public GameState currentGameState = GameState.menu;





    void Start()
    {
        // StartGame();
        BackToMenu();
        menuCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
        gamePause.SetActive(false);
    }

    private void Update()
    {
        /*
        //solo si pulsamos la tecla S empezara el juego
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartGame();
        }
        */

        //de esta manera solo reiniciara si no esta en juego
        if (Input.GetButtonDown("Start") && currentGameState != GameState.inGame /*&& this.currentGameState != GameState.inGame*/)
        {
            if(currentGameState == GameState.pause)
            {
                ResumeGame();
                return;
            }

            StartGame();
        }

        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
        if (Input.GetButtonDown("Reset"))
        {
            ResetGame();
        }
        
        //este codigo solo se ejecutara si estamos en el editor de Unity
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
#endif

    }

    public void StartGame()
    {
        CameraFollow cameraFollow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        if (PlayerController.sharedInstance.transform.position.x > 5)
        {
            LevelGenerator.sharedInstance.RemoveAllBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }
        PlayerController.sharedInstance.StartGame();
            cameraFollow.ResetCameraPosition();
            SetGameState(GameState.inGame);
        collectedObjects = 0;
    }


    void ResetGame()
    {
        GameOver();
        StartGame();
    }

    public void ResumeGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    public void Pause()
    {
        SetGameState(GameState.pause);
    }

 

    //hacemos un metodo nuevo que nos permita caviar el estado de juego en el que nos encontramos recibiendo como parametro el nuevo estado del juego
    void SetGameState(GameState newGameState){
        if(newGameState == GameState.inGame)
        {
            //hay que preparar la escena para jugar
            menuCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            gameCanvas.SetActive(true);
            gamePause.SetActive(false);
            Time.timeScale = 1.0f;
        }else if (newGameState == GameState.menu)
        {
            //hay que preparar la escena para mostrar el menu
            menuCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            gamePause.SetActive(false);
            Time.timeScale = 0.0f;

        }else if(newGameState == GameState.gameOver){
            // hay que preparar el juego para mostrar el fin de partida
            menuCanvas.SetActive(false);
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);
            gamePause.SetActive(false);
            Time.timeScale = 0.0f;

        }else if(newGameState == GameState.pause)
        {
            menuCanvas.SetActive(false);
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            gamePause.SetActive(true);
            Time.timeScale = 0.0f;
        }



        //asignamos el estado actual al que nos ha llegado por parametro
        this.currentGameState = newGameState;
        }

    public void ExitGame()
    {
        //es un if else que depende de la plataforma donde se ejecute
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void CollectObject(int objectValue)
    {
        this.collectedObjects += objectValue;
    }

}
