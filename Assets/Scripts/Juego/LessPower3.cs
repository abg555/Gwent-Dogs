using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessPower3 : MonoBehaviour
{

    public GameObject[] positions;
    public PowerZoneManager powerZoneManager;
    public PowerZoneManager powerZoneManager2;
    public PowerZoneManager powerZoneManager3;
    public GameManager gameManager;
    public void LessPower1()
    {
        Debug.Log("menosi");
        GameObject minPowerCard = null;
        float minPower = float.MaxValue;

        foreach (GameObject position in positions)
        {
            foreach (Transform child in position.transform)
            {
                Cardview cardvio = child.GetComponent<Cardview>();
                if (cardvio != null && cardvio.cardKind == 1 && cardvio.cardPower < minPower)
                {
                    minPower = cardvio.cardPower;
                    minPowerCard = child.gameObject;
                }
            }
        }

        if (minPowerCard != null)
        {
            Cardview cardView = minPowerCard.GetComponent<Cardview>();
            if (cardView != null)
            {
                Debug.Log("menii");
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

                gameManager.cementerys.Add(minPowerCard);
            }
            Destroy(minPowerCard);
        }
    }





}
