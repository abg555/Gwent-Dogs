using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPower6 : MonoBehaviour
{
    public GameObject as2;
    public PowerZoneManager2 powerZoneManager2;

    public void NPowerChau()
    {
        int sameCardNameCount = 0;
        string targetCardName = "Chau Chau Pescadu";
        Cardview specificCard = null;
        int newPower = 0;


        foreach (Transform child in as2.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardName == targetCardName && cardView.cardNumber != 53)
            {

                sameCardNameCount++;
                // Debug.Log("masss");


            }
            else if (cardView != null && cardView.cardNumber == 53)
            {
                specificCard = cardView;
            }


        }

        if (specificCard != null)
        {

            newPower = sameCardNameCount * specificCard.cardPower;



            Debug.Log(newPower);
        }

        powerZoneManager2.RemoveCardPower2("as2", specificCard.cardPower);
        powerZoneManager2.AddCardPower2("as2", newPower);
    }
}
