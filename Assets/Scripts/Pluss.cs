using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluss : MonoBehaviour
{

    public GameObject ased;
    public PowerZoneManager powerZoneManager;
    private List<GameObject> processedCards = new List<GameObject>();

    public void Plusss()
    {
        // Debug.Log("hola");
        foreach (Transform child in ased.transform)// busca en los hijos de ased
        {
            GameObject card = child.gameObject;// definen un nuevo objeto card que es un hijo de ased, y luego verifica si card ya ha sido procesado
            if (!processedCards.Contains(card))
            {
                Cardview cardView = card.GetComponent<Cardview>();
                if (cardView != null && cardView.cardKind == 1)
                {
                    cardView.cardPower += 1;// incrementa el cardPower a 1
                    Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);

                    powerZoneManager.AddCardPower(cardView.cardZone, 1); //actualiza el cardPower


                    processedCards.Add(card); //agrega la carta a la lista
                }
            }
        }

    }
}


