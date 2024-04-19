using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePower : MonoBehaviour
{
    public GameObject[] positions;
    public PowerZoneManager2 powerZoneManager;
    public PowerZoneManager2 powerZoneManager2;
    public PowerZoneManager2 powerZoneManager3;
    public GameManager gameManager;


    public void MorePower1()
    {

        Debug.Log("masi");
        GameObject maxPowerCard = null;
        float maxPower = float.MinValue;

        foreach (GameObject position in positions)
        {
            foreach (Transform child in position.transform)
            {
                Cardview cardvio = child.GetComponent<Cardview>();
                if (cardvio != null && cardvio.cardKind == 1 && cardvio.cardPower > maxPower)
                {
                    maxPower = cardvio.cardPower;
                    maxPowerCard = child.gameObject;
                }
            }
        }

        if (maxPowerCard != null)
        {
            Cardview cardView = maxPowerCard.GetComponent<Cardview>();
            if (cardView != null)
            {
                Debug.Log("maii");
                int powerToRemove = cardView.cardPower;
                cardView.cardPower = 0;
                if (cardView.cardZone == "c2")
                {
                    powerZoneManager.RemoveCardPower2(cardView.cardZone, powerToRemove);
                }
                if (cardView.cardZone == "ar2")
                {
                    powerZoneManager2.RemoveCardPower2(cardView.cardZone, powerToRemove);
                }
                if (cardView.cardZone == "as2")
                {
                    powerZoneManager3.RemoveCardPower2(cardView.cardZone, powerToRemove);
                }

                gameManager.cementerys.Add(maxPowerCard);
            }
            Destroy(maxPowerCard);



        }

    }
}
