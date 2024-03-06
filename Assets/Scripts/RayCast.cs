using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float factorEscala = 1.2f; // Factor de escala al hacer click izquierdo

    void Start(){
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
    }
    
    void Update()
    {
        // Verificar si se ha presionado el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            // Obtener el punto de impacto del rayo desde la cámara al mundo
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Verificar si el rayo colisiona con un objeto
            if (Physics.Raycast(ray, out hit))
            {
                // Verificar si el objeto golpeado es el mismo objeto que contiene este script
                if (hit.collider.gameObject == gameObject)
                {
                    // Escalar el objeto
                    transform.localScale *= factorEscala;
                }
            }
        }
    }
}



