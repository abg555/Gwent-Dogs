using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
    public GameObject Canvas;
    private GameObject zoomCard;
    private Vector2 zoomPlace = new Vector2(5, 5);

    public void Awake()
    {
        Canvas = GameObject.Find("MainCanvas");

    }
    public void OneHoverEnter()
    {
        zoomCard = Instantiate(gameObject, new Vector2(110, 157), Quaternion.identity);
        zoomCard.transform.SetParent(Canvas.transform, false);
        zoomCard.transform.localScale = zoomPlace;
        /*zoomCard.layer = LayerMask.NameToLayer("Zoom");

        RectTransform rect = zoomCard.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(186, 207);*/
    }
    public void OnHoverExit()
    {
        Destroy(zoomCard);
    }
}
