using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Average2 : MonoBehaviour
{

    public GameObject c;
    public GameObject c2;

    public PowerZoneManager powerZoneManager;


    public void Average1()
    {
        int powerZone = SumCardPowers(c2);
        int powerZones = SumCardPowers(c);
        int child = c2.transform.childCount;
        if (child != 0)
        {
            int totalPower = powerZone / child;
            powerZoneManager.RemoveCardPower("c", powerZones);
            powerZoneManager.AddCardPower("c", totalPower);
        }
    }

    public int SumCardPowers(GameObject zone)
    {
        int totalPower = 0;

        foreach (Transform child in zone.transform)
        {
            Cardview card = child.GetComponent<Cardview>();
            if (card != null)
            {
                totalPower += card.cardPower;
            }
        }

        return totalPower;
    }


}

