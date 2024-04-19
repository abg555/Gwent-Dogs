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
        Canvas = GameObject.Find("MainCanvas");
        turnButton = GameObject.FindObjectOfType<TurnButton>();

    }

    void Update()
    {
        if (isDragging && !isPlace)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        dropZone = null;
    }
    public void StartDrag()
    {
        if (!isPlace)
        {
            startParent = transform.parent.gameObject;
            startPosition = transform.position;
            isDragging = true;



        }

    }
    public void Enddrag()
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
    public bool ZoneSpace()
    {
        Zone conditions = dropZone.GetComponent<Zone>();
        string word = conditions.zoneNames;
        string ca = gameObject.GetComponent<Cardview>().cardZone;
        string ca1 = gameObject.GetComponent<Cardview>().cardZone2;

        if (word == ca || word == ca1) return true;
        else return false;
    }

}
