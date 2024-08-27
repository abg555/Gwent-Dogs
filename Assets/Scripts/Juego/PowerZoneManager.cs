using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerZoneManager : MonoBehaviour
{
    public List<string> specificZones = new List<string> { "c", "ar", "as" };
    public Dictionary<string, int> zonePowers = new Dictionary<string, int>();
    public TurnManager zone;
    //definen las variables de la clase. specificZones es una lista de zonas específicas. zonePowers es un diccionario que mapea el nombre de una zona a su poder. zone es una instancia de la clase TurnManager
    void Start()
    {
        foreach (var zone in specificZones)
        {
            zonePowers[zone] = 0;
        }// se inicializa el poder de todas las zonas específicas a 0
    }

    public void SumInDropZone()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Cardview>().cardPower++;


            // child.GetComponent<Cardview>().powerText.text. ++.ToString();
        }
    }



    public void AddCardPower(string zoneName, int power)
    {//Esta función añade poder a una zona específica. Si zoneName está en specificZones, incrementa el poder de la zona en power, llama a la función AddZonePower de zone
        if (specificZones.Contains(zoneName))
        {
            zonePowers[zoneName] += power;
            zone.AddZonePower(zoneName, power);
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }

    public void RemoveCardPower(string zoneName, int power)
    {
        if (specificZones.Contains(zoneName))
        {//Esta función añade poder a una zona específica. Si zoneName está en specificZones, disminuye el poder de la zona en power, llama a la función AddZonePower de zone
            zonePowers[zoneName] -= power;
            zone.RemoveZonePower(zoneName, power);
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }

}

