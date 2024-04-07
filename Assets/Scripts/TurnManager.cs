using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;
using static UnityEngine.Vector2;
using static System.Numerics.Vector2;



public class TurnManager : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;
    public List<string> specificZones = new List<string> { "c", "ar", "as" };
    public Dictionary<string, int> zonePowers = new Dictionary<string, int>();
    public Sprite back;
    private bool isPlayerTurn = true; // Asume que el turno comienza con el jugador


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
        // Al inicio del juego, solo las cartas del jugador son visibles e interactuables
        SetPlayerAreaVisibility(true);
        SetEnemyAreaVisibility(false);
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
            // Turno del jugador
            SetPlayerAreaVisibility(true);
            SetEnemyAreaVisibility(false);
        }
        else
        {
            // Turno del enemigo
            SetPlayerAreaVisibility(false);
            SetEnemyAreaVisibility(true);
            CoverEnemyCards();
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
    private void CoverEnemyCards()
    {
        foreach (Transform card in enemyArea.transform)
        {
            // Crea un nuevo objeto de imagen para cada carta del enemigo
            GameObject cover = new GameObject("Cover");
            cover.transform.SetParent(card.transform, false);
            cover.transform.localPosition = new UnityEngine.Vector2(0, 0); // Ajusta la posición según sea necesario
            cover.transform.localScale = new UnityEngine.Vector2(1, 1); // Ajusta la escala según sea necesario

            // Agrega un componente de imagen al objeto de la foto y asigna la imagen
            SpriteRenderer spriteRenderer = cover.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = back;
        }
    }
}
