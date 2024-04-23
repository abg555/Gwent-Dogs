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
        int child2 = c.transform.childCount;
        if (child != 0 && child2 != 0)
        {
            int totalPower = powerZone / child;
            int tt = totalPower / child2;
            foreach (Transform chil in c2.transform)
            {
                Cardview card = chil.GetComponent<Cardview>();
                if (card != null)
                {
                    card.cardPower = tt;
                }
            }
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

