using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    //como de lejos va a seguir al jugador, un pequeño espacio tanto en x como en y
    public Vector3 offset = new Vector3(0.1f, 0.0f, -10.0f);
    //pequeño tiempo en que la camara va estar quieta durante el inicio y luego empazará a seguir
    public float dampTime = 0f;
    //la velocidad de la cámara
    public Vector3 velocity = Vector3.zero;

    private Vector3 destination;

    // Start is called before the first frame update
    void Awake()
    {

        //que la actualización de frames sea constante
        //esto indica que Unity intente renderizar al número de frames que yo le indique
        //se lo indicamos a la camara, ya que es ella quien renderiza
       Application.targetFrameRate = 60;
        
    }

    // Update is called once per frame
    void Update()
    {//defininos esta variable para ahhorarnos codigo de mas

        //que siga al target con los parametros que hemos indicado más arriba
        //pasamos las coordenadas del juego a las de la camara, el punto que sea donde esta el personaje lo transformamos a coordenadas de pantalla
        //transformamos las coordenadas del personaje en coordenadas del viewport, lo que ve la camara, como esta el personaje repecto al viewport
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        //el incremento, cuanto tiene que moverse la camara, para que el personaje sig en las coordenadas del viewport
        //también hay que calcular como esta la camara respecto al mundo, al reves que antes
        //donde quiero ir menos donde esta ahora mismo, la cantidad de movimiento que se tiene que mover cada frame
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        //el punto desde el que partiamos, donde estaba más un pequeño incremento
        destination = point + delta;
        //ahora vamos a corregir para que la camara no vaya subiendo y bajando, la camara solo se movera ex¡n x, la y se mantendà fija
        destination = new Vector3(target.position.x, offset.y, offset.z);

        //asignamos la posicion de la camara a la nueva destinación y con ayuda del SmoothDamp la camara se movera de manera suave, de forma continua
        //rl smoothDamp necesita los parametros de donde esta ahora y donde tiene que ir, con que velocidad
        //por si la velocidad cambia el metodo SmoothDamp la suavizara
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
    }

    public  void ResetCameraPosition()
    {
        //defininos esta variable para ahhorarnos codigo de mas

        //que siga al target con los parametros que hemos indicado más arriba
        //pasamos las coordenadas del juego a las de la camara, el punto que sea donde esta el personaje lo transformamos a coordenadas de pantalla
        //transformamos las coordenadas del personaje en coordenadas del viewport, lo que ve la camara, como esta el personaje repecto al viewport
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        //el incremento, cuanto tiene que moverse la camara, para que el personaje sig en las coordenadas del viewport
        //también hay que calcular como esta la camara respecto al mundo, al reves que antes
        //donde quiero ir menos donde esta ahora mismo, la cantidad de movimiento que se tiene que mover cada frame
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        //el punto desde el que partiamos, donde estaba más un pequeño incremento
        destination = point + delta;
        //ahora vamos a corregir para que la camara no vaya subiendo y bajando, la camara solo se movera ex¡n x, la y se mantendà fija
        destination = new Vector3(target.position.x, offset.y, offset.z);
        this.transform.position = destination;
    }
}
