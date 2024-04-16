using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;
    public bool isCard = false;
    public bool isPlayerTurn = true;


    // Start is called before the first frame update
    void Start()
    {

        SetPlayerAreaVisibility(false);
        SetEnemyAreaVisibility(false);
    }
    public void OnClick()
    {
        ChangeTurn();
    }
    public void ChangeTurn()
    {
        isPlayerTurn = !isPlayerTurn;
        isCard = false;

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
        if (!isCard)
        {
            foreach (Transform card in playerArea.transform)
            {
                card.gameObject.SetActive(isVisible);
                card.GetComponent<Drag>().enabled = isVisible;

            }
        }
    }


    private void SetEnemyAreaVisibility(bool isVisible)
    {

        if (!isCard)
        {
            foreach (Transform card in enemyArea.transform)
            {
                card.gameObject.SetActive(isVisible);
                card.GetComponent<Drag>().enabled = isVisible;
            }

        }
    }
    public void CardPlayed()
    {
        isCard = true;
    }
}
