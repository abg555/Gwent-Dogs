using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pluss2 : MonoBehaviour

{
    public GameObject ased;
    public PowerZoneManager powerZoneManager;
    private List<GameObject> processedCards = new List<GameObject>();

    public void Plusss2()
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
                    cardView.cardPower += 1;
                    Debug.Log("El nuevo valor de cardPower para " + cardView.cardName + " es: " + cardView.cardPower);

                    powerZoneManager.AddCardPower(cardView.cardZone, 1);


                    processedCards.Add(card);
                }
            }
        }

    }
}
