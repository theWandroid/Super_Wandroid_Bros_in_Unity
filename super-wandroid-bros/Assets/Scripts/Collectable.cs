using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healthPotion,
    manaPotion,
    money
}
public class Collectable : MonoBehaviour
{
    public CollectableType type = CollectableType.money;

    //variable para saber si la moneda ha sido recogida o no
    bool isCollected = false;
    public int value;
    public AudioClip collectSound;
    AudioSource audio;

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //metodo para mostrar la moneda
    void Show()
    {
        //activamos la imagen de la moneda
        this.GetComponent<SpriteRenderer>().enabled = true;
        //activa el collider de la moneda para poder ser recogida
        this.GetComponent<CircleCollider2D>().enabled = true;
        isCollected = false;
    }

    //metodo para desactivar la moneda
    void Hide()
    {
        //ocultamos la imagen de la moneda
        this.GetComponent<SpriteRenderer>().enabled = false;
        //activa el collider de la moneda para poder ser recogida
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    //metodo para recoger la moneda
    void Collect()
    {
        isCollected = true;
        Hide();

        audio = GetComponent<AudioSource>();
        //esto evitara errores, solo se reproducira si tiene AudioSource y tiene un clip de audio
        if(audio != null && this.collectSound != null)
        {
            audio.PlayOneShot(this.collectSound);
        }

        switch (this.type)
        {
            case CollectableType.money:
                GameManager.sharedInstance.CollectObject(value);
                break;
            case CollectableType.healthPotion:
                //dar vida al jugador
                PlayerController.sharedInstance.CollectHealth(value);
                break;
            case CollectableType.manaPotion:
                //dar mana al jugador
                PlayerController.sharedInstance.CollectMana(value);
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.gameObject.tag == "Player")
        {
            Collect();
        }
    }
}
