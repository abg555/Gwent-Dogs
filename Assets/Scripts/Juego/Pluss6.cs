using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluss6 : MonoBehaviour
{
    public GameObject ased;
    public PowerZoneManager2 powerZoneManager2;
    private List<GameObject> processedCards = new List<GameObject>();

    public void Plusss3()
    {
        // Debug.Log("hola");
        foreach (Transform child in ased.transform)
        {
            GameObject card = child.gameObject;
            if (!processedCards.Contains(card))
            {
                Cardview cardView = card.GetComponent<Cardview>();
                if (cardView != null && cardView.cardKind == 1)
                {
                    cardView.cardPower += 2;
                    Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);

                    powerZoneManager2.AddCardPower2(cardView.cardZone, 2);


                    processedCards.Add(card);
                }
            }
        }

    }
}
