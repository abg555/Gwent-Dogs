using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;
using static UnityEngine.Vector2;
using static System.Numerics.Vector2;
using Unity.Mathematics;



public class TurnManager : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;
    public List<string> specificZones = new List<string> { "c", "ar", "as" };
    public Dictionary<string, int> zonePowers = new Dictionary<string, int>();

    public bool isPlayerTurn = true;


    public void AddZonePower(string zoneName, int power)
    {
        if (specificZones.Contains(zoneName))
        {
            zonePowers[zoneName] += power;
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }

    public void RemoveZonePower(string zoneName, int power)
    {
        if (specificZones.Contains(zoneName))
        {
            zonePowers[zoneName] -= power;
            Debug.Log("Poder en zona " + zoneName + ": " + zonePowers[zoneName]);
        }
        else
        {
            Debug.Log("La zona " + zoneName + " no está en la lista de zonas específicas del jugador.");
        }
    }

    void Start()
    {



        foreach (var zone in specificZones)
        {
            zonePowers[zone] = 0;
        }
    }

    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn; // Cambia el turno

        if (isPlayerTurn)
        {

            SetPlayerAreaVisibility(true);
            SetEnemyAreaVisibility(false);
        }
        else
        {

            SetPlayerAreaVisibility(false);
            SetEnemyAreaVisibility(true);

        }

        Debug.Log("Turno actual: " + (isPlayerTurn ? "Jugador" : "Enemigo"));
    }

    private void SetPlayerAreaVisibility(bool isVisible)
    {
        foreach (Transform card in playerArea.transform)
        {
            card.gameObject.SetActive(isVisible);
            card.GetComponent<Drag>().enabled = isVisible;
        }
    }

    private void SetEnemyAreaVisibility(bool isVisible)
    {
        foreach (Transform card in enemyArea.transform)
        {
            card.gameObject.SetActive(isVisible);
            card.GetComponent<Drag>().enabled = isVisible;
        }
    }

}
