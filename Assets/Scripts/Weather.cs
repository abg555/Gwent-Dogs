using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour

{


    public GameObject ased;
    public PowerZoneManager powerZoneManager;
    public PowerZoneManager2 powerZoneManager1;

    public GameObject ased1;
    private List<GameObject> processedCards = new List<GameObject>();
    private List<GameObject> processedCards1 = new List<GameObject>();

    public void Weather1()
    {
        // Debug.Log("hola");
        foreach (Transform child in ased.transform)//busca en los hijos de ased
        {
            GameObject card = child.gameObject;
            if (!processedCards.Contains(card))// definen un nuevo objeto card que es un hijo de ased, y luego verifica si card ya ha sido procesado
            {
                Cardview cardView = card.GetComponent<Cardview>();
                if (cardView != null && cardView.cardKind == 1)
                {
                    cardView.cardPower -= 1;// dismunuye el poder en 1
                    Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);

                    powerZoneManager.AddCardPower(cardView.cardZone, -1);//actualiza el poder


                    processedCards.Add(card);//agrega la carta a la lista
                }
            }
        }

        foreach (Transform child in ased1.transform)
        {
            GameObject card = child.gameObject;
            if (!processedCards1.Contains(card))
            {
                Cardview cardView = card.GetComponent<Cardview>();
                if (cardView != null && cardView.cardKind == 1)
                {
                    cardView.cardPower -= 1;
                    Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);

                    powerZoneManager1.AddCardPower2(cardView.cardZone, -1);


                    processedCards1.Add(card);
                }
            }
        }

    }
}


