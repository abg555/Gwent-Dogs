using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    private GameObject zoomCard;     /*se utilizará para almacenar la carta que se está ampliando*/
    private Vector2 zoomPlace = new Vector2(5, 5);    /*define la escala inicial de la carta ampliada.*/

    public void Awake()
    {
        Canvas = GameObject.Find("MainCanvas");   /* Busca en la escena un objeto de juego con el nombre "MainCanvas"*/

    }
    public void OneHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(110, 157), Quaternion.identity);  /*Crea una instancia de la carta actual y la asigna a zoomCard*/
        zoomCard.transform.SetParent(Canvas.transform, false); /*establece Canvas como padre de zoomCard*/
        zoomCard.transform.localScale = zoomPlace; /*establece la escala de zoomCrad a zoomPlace*/

    }
    public void OnHoverExit()
    {
        Destroy(zoomCard); /*destruye el zoomCard*/
    }
}
