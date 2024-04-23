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


        GameObject minChildZone = null; //inicializan variables para encontrar la zona con el recuento de hijos mínimo. minChildZone almacenará la referencia a la zona con menos objetos secundarios, y minChildCount se establece inicialmente en el valor entero máximo posible.
        int minChildCount = Int32.MaxValue;


        foreach (GameObject zone in zones) //busca a travez de cada GameObject de zones
        {
            int childCount = zone.transform.childCount;
            Debug.Log("Zona: " + zone.name + ", Hijos: " + childCount);
            if (childCount > 0 && childCount < minChildCount)
            {
                minChildCount = childCount;
                minChildZone = zone;
            }
        }//cuentan el número de hijos en cada zone, luego verifican si el número de hijos es mayor que 0 y menor que minChildCount. Si es así, actualizan minChildCount y minChildZone


        if (minChildZone != null)
        {

            string zoneName = minChildZone.name;
            Debug.Log("Zona seleccionada: " + zoneName);
            foreach (Transform child in minChildZone.transform)
            {
                Cardview cardView = child.GetComponent<Cardview>();
                if (cardView != null)
                {
                    cardView.cardPower = 0;
                }
            }
            //obtienen el nombre de minChildZone,  recorren todos los hijos en minChildZone, obtienen el componente Cardview de cada hijo, y si cardView no es null, establecen cardPower a 0


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
