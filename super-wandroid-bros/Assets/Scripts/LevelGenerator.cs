using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    //solo puede haber un generador de niveles, por eso hacemos un singleton
    public static LevelGenerator sharedInstance;

    //una lista de todos los bloques disponibles
    public List <LevelBlock> allTheLevelBlocks = new List<LevelBlock>();

    //el punto de inicio, para saber donde generar el nivel
    public Transform levelStartPoint;
//una lista de los niveles que hay en la escena en el momento concreto, que se ira rellenando dinamicamente
    public List<LevelBlock> currentBlocks = new List<LevelBlock>();

    public LevelBlock firstBlock;
 

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        GenerateInitialBlocks();


    }

    public void GenerateInitialBlocks()
    {

        
        for(int i = 0; i < 2; i++)
        {
            AdddLevelBlocK();

        }
    }

    //para hacer niveles proceduralmente
 public void AdddLevelBlocK()
    {
        //genera un numero aleatorio entre los dos parametros que le estamos pasando, el primero lo cogera, pero el segundo no
        int randomIndex = Random.Range(0, allTheLevelBlocks.Count);
        //estp es para instanciar el bloque actual 
        LevelBlock currentBlock;
           

        //ahora vamos a decir donde lo instanciamos
        Vector3 spawnPosition = Vector3.zero;

        //si aún no se han generado bloques se generaran a partir del lugar que hemos creado como el punto inicial
        if(currentBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstBlock);
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoint.position;
        }
        //si ya se han generado bloques entonces el punto donde se generaran será a paratir del exitPoint del último bloque que hemos creado
        else
        {
            //hacemos un casting, ya que cuando instanciamos se nos genera un GameObject, es decir convierte a LevelBlock el gameObject que estamos generando por instanciación
            currentBlock = (LevelBlock)
            Instantiate(allTheLevelBlocks[randomIndex]);
            //y lo generaremos como hijo del generador de niveles
            currentBlock.transform.SetParent(this.transform);
            spawnPosition = currentBlocks[currentBlocks.Count - 1].exitPoint.position;
        }
       Vector3 correction = new Vector3(spawnPosition.x - currentBlock.startPoint.position.x, spawnPosition.y - currentBlock.startPoint.position.y, 0);

        //ahora lo unico que me queda por hacer es ponerlo en su posicion correspondiente
         currentBlock.transform.position =correction;
        //y añadimos a la colleccion el ultimo bloque generado proceduralmente
        currentBlocks.Add(currentBlock);
        

    }

    public void RemoveOldestBlock()
    {
        LevelBlock oldestBlock = currentBlocks[0];
        currentBlocks.Remove(oldestBlock);
        Destroy(oldestBlock.gameObject);
    }

    public void RemoveAllBlocks()
    {
        while(currentBlocks.Count > 0)
        {
            RemoveOldestBlock();
        }
    }
}
