using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drag : MonoBehaviour
{
    private bool isDragging = false;
    private bool isOverDropZone = false;
    private bool isPlace = false;
    private GameObject dropZone;
    private GameObject Canvas;
    private GameObject startParent;
    private Vector2 startPosition;

    private void Awake()
    {
        Canvas = GameObject.Find("MainCanvas");
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
        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);
            isPlace = true;
        }
        else
        {
            transform.position = startPosition;
            transform.SetParent(startParent.transform, false);
        }
    }
}
