using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public LayerMask layerMask;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;
        Debug.Log("Drag");
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, layerMask);

        // Si el rayo golpea un objeto
        if (hit.collider != null)
        {
            // hit.transform contiene el objeto golpeado por el rayo
            GameObject objetoDebajoDelMouse = hit.transform.gameObject;

            // Ahora puedes hacer algo con el objeto debajo del mouse
            Debug.Log("Objeto debajo del mouse: " + objetoDebajoDelMouse.name);

        }
        // Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, Mathf.Infinity, LayerMask.GetMask("CardContainer"));

        // if (hit.collider != null)
        // {
        //     GameObject targetObject = hit.transform.gameObject;

        //     print(targetObject);
        // }
        print("EndDrag");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
