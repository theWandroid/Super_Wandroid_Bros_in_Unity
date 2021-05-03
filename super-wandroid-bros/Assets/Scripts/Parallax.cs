using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float speed = 0.0f;
    private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //para poder llevar a cabo el efecto parallax añadimos un componente rigidbody a las imagenes que hemos añadido de fondo a la camara
        this.rigidbody.velocity = new Vector2(speed, 0);

        //es una variable que creamos para saber la posicion del padre
        float parentPosition = this.transform.parent.transform.position.x;

        //para crear un forndo tipo parallax debemos de jugar con coordenadas relativas
        //para utilizar las coordenadas relativas debemos saber la  posición del padre en todo momento
        //nos interesa que las imagenes de fondo desaparezcan llegado una posicion determinada
        if(this.transform.position.x -parentPosition >=  33.4f)
        {
            //cuando la imagen llega a esa posicion, le decimos que se vuelva a una posicion anterior teniendo como referencia al padre
            this.transform.position = new Vector3(parentPosition - 34.11f, this.transform.position.y, this.transform.position.z);
        }
    }
}
