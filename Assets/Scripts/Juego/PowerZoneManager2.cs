using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerZoneManager2 : MonoBehaviour
{
    public List<string> specificZones2 = new List<string> { "c2", "ar2", "as2" };
    public Dictionary<string, int> zonePowers2 = new Dictionary<string, int>();
    public turn2 zone;
    void Start()
    {
        foreach (var zone in specificZones2)
        {
            zonePowers2[zone] = 0;
        }
    }
    public void AddCardPower2(string zoneName, int power)
    {
        if (specificZones2.Contains(zoneName))
        {
            zonePowers2[zoneName] += power;
            zone.AddZonePower2(zoneName, power);
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers2[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }

    public void RemoveCardPower2(string zoneName, int power)
    {
        if (specificZones2.Contains(zoneName))
        {
            zonePowers2[zoneName] -= power;
            zone.RemoveZonePower2(zoneName, power);
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers2[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }
    public void ResetPower()
    {
        foreach (var zone in specificZones2)
        {
            Debug.Log("holoooo");
            zonePowers2[zone] = 0;
        }
    }
}


