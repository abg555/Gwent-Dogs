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
        int powerZone = SumCardPowers(c);         /*powerZone y PowerZones almacenan los resultados en powerZone y powerZones, respectivamente*/
        int powerZones = SumCardPowers(c2);
        int child = c.transform.childCount;       /*child y child2 almacenan la cantidad de hijos de c y c2, respectivamente*/
        int child2 = c2.transform.childCount;
        if (child != 0 && child2 != 0)              /*verifica si c y c2 tiene hijos*/
        {
            int totalPower = powerZone / child;       /*almacena el promedio de poder de c*/
            int tt = totalPower / child2;              /*almacena el promedio del promdeio de c*/
            foreach (Transform chil in c2.transform)     /*recorre todos los hijos de c2*/
            {
                Cardview card = chil.GetComponent<Cardview>();      /*almacena su cardview*/
                if (card != null)
                {                                             /*actualiza su cardPower a tt*/
                    card.cardPower = tt;
                }
            }
            powerZoneManager2.RemoveCardPower2("c2", powerZones);      /*elimina el poder de las cartas en c2 y luego agrega el poder promedio al objeto c2*/
            powerZoneManager2.AddCardPower2("c2", totalPower);
        }





    }

    public int SumCardPowers(GameObject zone)
    {
        int totalPower = 0;

        foreach (Transform child in zone.transform)  /*recorre todos los hijos del objeto zone*/
        {
            Cardview card = child.GetComponent<Cardview>();     /*almacena el componente Cardview del hijo, verifican si el componente existe y, si es así, agregan su poder a totalPower*/
            if (card != null)
            {
                totalPower += card.cardPower;
            }
        }

        return totalPower;                /*devuelve el poder total de todas las cartas en la zona*/
    }

}
