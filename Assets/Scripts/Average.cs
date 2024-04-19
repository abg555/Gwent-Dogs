using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Average : MonoBehaviour
{
    public GameObject c;
    public GameObject c2;

    public PowerZoneManager2 powerZoneManager2;


    public void Average1()
    {
        int powerZone = SumCardPowers(c);
        int powerZones = SumCardPowers(c2);
        int child = c.transform.childCount;
        if (child != 0)
        {
            int totalPower = powerZone / child;
            powerZoneManager2.RemoveCardPower2("c2", powerZones);
            powerZoneManager2.AddCardPower2("c2", totalPower);
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
