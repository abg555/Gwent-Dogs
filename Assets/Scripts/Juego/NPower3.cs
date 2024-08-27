using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPower3 : MonoBehaviour
{
    public GameObject as1;
    public PowerZoneManager powerZoneManager;

    public void NPowerChau()
    {
        int sameCardNameCount = 0;
        string targetCardName = "Chau Chau Pescadu";
        Cardview specificCard = null;
        int newPower = 0;


        foreach (Transform child in as1.transform)
        {
            Cardview cardView = child.GetComponent<Cardview>();
            if (cardView != null && cardView.cardName == targetCardName && cardView.cardNumber != 52)
            {

                sameCardNameCount++;
                // Debug.Log("masss");


            }
            else if (cardView != null && cardView.cardNumber == 52)
            {
                specificCard = cardView;
            }


        }

        if (specificCard != null)
        {

            newPower = sameCardNameCount * specificCard.cardPower;



            Debug.Log(newPower);
        }

        powerZoneManager.RemoveCardPower("as", specificCard.cardPower);
        powerZoneManager.AddCardPower("as", newPower);
    }
}
