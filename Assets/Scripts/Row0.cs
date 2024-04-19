using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row0 : MonoBehaviour
{
    public GameObject[] zones;
    public PowerZoneManager2 powerZoneManager;
    public PowerZoneManager2 powerZoneManager2;
    public PowerZoneManager2 powerZoneManager3;



    public void CleamRow()
    {


        GameObject minChildZone = null;
        int minChildCount = Int32.MaxValue;


        foreach (GameObject zone in zones)
        {
            int childCount = zone.transform.childCount;
            Debug.Log("Zona: " + zone.name + ", Hijos: " + childCount);
            if (childCount > 0 && childCount < minChildCount)
            {
                minChildCount = childCount;
                minChildZone = zone;
            }
        }


        if (minChildZone != null)
        {

            string zoneName = minChildZone.name;
            Debug.Log("Zona seleccionada: " + zoneName);

            if (powerZoneManager.specificZones2.Contains(zoneName))
            {
                // Obtiene el poder actual de la zona
                int currentPower = powerZoneManager.zonePowers2[zoneName];

                // Establece el poder de la fila de la zona a 0
                powerZoneManager.RemoveCardPower2(zoneName, currentPower);
            }
            else
            {
                Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
            }



        }
    }
}
