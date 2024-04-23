using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drag : MonoBehaviour
{

    public bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isPlace = false;
    private bool isSum = false;
    private GameObject dropZone;
    private GameObject Canvas;
    private GameObject startParent;


    private Vector2 startPosition;
    private TurnButton turnButton;


    private void Awake()
    {
        Canvas = GameObject.Find("MainCanvas"); /* Busca en la escena un objeto de juego con el nombre "MainCanvas"*/
        turnButton = GameObject.FindObjectOfType<TurnButton>();

    }

    void Update() //Si isDragging es true y isPlace es false, este método mueve la carta a la posición del cursor del ratón y cambia su padre al objeto Canvas.
    {
        if (isDragging && !isPlace)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //establece isOverDropZone en true y guarda la carta con la que está colisionando en dropZone
    {
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)//establece isOverDropZone en false y borra la referencia a dropZone
    {
        isOverDropZone = false;
        dropZone = null;
    }
    public void StartDrag() //se llama para comenzar a arrastrar la carta. Guarda el padre original y la posición original de la carta, y establece isDragging en true.
    {
        if (!isPlace)
        {
            startParent = transform.parent.gameObject;
            startPosition = transform.position;
            isDragging = true;



        }

    }
    public void Enddrag()  //se llama para terminar de arrastrar la carta. Si la carta se soltó sobre una zona válida (isOverDropZone es true y ZoneSpace() devuelve true), la carta se mueve a esa zona, se llama al método ChangeTurn del turnButton, y se actualiza el poder de la zona. Si la carta no se soltó sobre una zona válida, se mueve de vuelta a su posición original.
    {
        isDragging = false;


        if (isOverDropZone && ZoneSpace())
        {
            if (!isPlace)
            {
                turnButton = GameObject.Find("Button").GetComponent<TurnButton>();
                turnButton.ChangeTurn();
            }
            transform.SetParent(dropZone.transform, false);
            isPlace = true;

            PowerZoneManager zonePowerManager = dropZone.GetComponent<PowerZoneManager>();
            PowerZoneManager2 zonePowerManager2 = dropZone.GetComponent<PowerZoneManager2>();





            if (zonePowerManager != null && !isSum)
            {

                int cardPower = GetComponent<Cardview>().cardPower;

                zonePowerManager.AddCardPower("c", cardPower);
                isSum = true;



            }
            if (zonePowerManager2 != null && !isSum)
            {

                int cardPower = GetComponent<Cardview>().cardPower;


                zonePowerManager2.AddCardPower2("c2", cardPower);
                isSum = true;


            }



        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);

        }
    }
    public bool ZoneSpace()  //verifica si la zona en la que se soltó la carta es válida para este objeto. Devuelve true si el nombre de la zona coincide con cardZone o cardZone2 de la carta, y false en caso contrario
    {
        Zone conditions = dropZone.GetComponent<Zone>();
        string word = conditions.zoneNames;
        string ca = gameObject.GetComponent<Cardview>().cardZone;
        string ca1 = gameObject.GetComponent<Cardview>().cardZone2;

        if (word == ca || word == ca1) return true;
        else return false;
    }

}
