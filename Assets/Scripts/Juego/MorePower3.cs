using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorePower3 : MonoBehaviour
{
    public GameObject[] positions;
    public PowerZoneManager powerZoneManager;
    public PowerZoneManager powerZoneManager2;
    public PowerZoneManager powerZoneManager3;
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
                if (cardView.cardZone == "c")
                {
                    powerZoneManager.RemoveCardPower(cardView.cardZone, powerToRemove);
                }
                if (cardView.cardZone == "ar")
                {
                    powerZoneManager2.RemoveCardPower(cardView.cardZone, powerToRemove);
                }
                if (cardView.cardZone == "as")
                {
                    powerZoneManager3.RemoveCardPower(cardView.cardZone, powerToRemove);
                }

                gameManager.cementerys.Add(maxPowerCard);
            }
            Destroy(maxPowerCard);



        }

    }
}
