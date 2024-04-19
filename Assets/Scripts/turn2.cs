using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turn2 : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;
    public List<string> specificZones2 = new List<string> { };
    public Dictionary<string, int> zonePowers2 = new Dictionary<string, int>();
    public bool isPlayerTurn = true;


    public void AddZonePower2(string zoneName, int power)
    {
        if (specificZones2.Contains(zoneName))
        {
            zonePowers2[zoneName] += power;
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers2[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }

    public void RemoveZonePower2(string zoneName, int power)
    {
        if (specificZones2.Contains(zoneName))
        {
            zonePowers2[zoneName] -= power;
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers2[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (var zone in specificZones2)
        {
            zonePowers2[zone] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
