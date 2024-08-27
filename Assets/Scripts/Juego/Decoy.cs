using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : MonoBehaviour
{
    public GameObject c;
    public GameObject playerArea;
    public PowerZoneManager powerZoneManager;



    public void DecoySalchicha()
    {
        int childCount = c.transform.childCount; /*obtiene el numero de hijos de c*/

        if (childCount > 0) /*verifica si tiene hijos*/
        {
            Transform randomChild = null;
            Cardview cardView = null;
            int attempts = 0;
            int maxAttempts = 15;


            while (attempts < maxAttempts) /*este bucle se ejcutara 15 veces*/
            {
                int randomIndex = Random.Range(0, childCount);
                randomChild = c.transform.GetChild(randomIndex);
                cardView = randomChild.GetComponent<Cardview>();
                if (cardView != null && cardView.cardNumber != 12 && cardView.cardKind == 1)
                {
                    break;
                }
                attempts++;
            } /*Estas líneas seleccionan un hijo aleatorio del objeto c, obtienen su componente Cardview y verifican si el número de la carta es distinto de 12 y si el tipo de la carta es 1. Si se cumplen estas condiciones, se rompe el bucle.*/

            if (cardView != null && cardView.cardNumber != 12 && cardView.cardKind == 1)
            {
                // Crea una nueva carta en playerArea que es una copia de la carta seleccionada

                GameObject newCard = Instantiate(randomChild.gameObject, playerArea.transform.position, Quaternion.identity);
                newCard.transform.SetParent(playerArea.transform, false);
                newCard.SetActive(false); //la hace invisible

                // Destruye la carta original en c
                powerZoneManager.RemoveCardPower("c", cardView.cardPower); //elimina su poder 
                Destroy(randomChild.gameObject);
            }
        }
        else
        {
            Debug.Log("No hay más cartas en c para dibujar.");
        }
    }
}

