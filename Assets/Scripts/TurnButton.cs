using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TurnButton : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;

    public bool isPlayerTurn = true;
    public Drag drag;

    public bool shouldChangeTurn = true;



    // Start is called before the first frame update
    void Start()
    {

        ChangeTurn();
    }

    public void ChangeTurn()
    {


        isPlayerTurn = !isPlayerTurn;



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
    public void SetPlayerAreaVisibility(bool isVisible)
    {


        foreach (Transform card in playerArea.transform)
        {
            card.gameObject.SetActive(isVisible);
            card.GetComponent<Drag>().enabled = isVisible;

        }

    }


    public void SetEnemyAreaVisibility(bool isVisible)
    {



        foreach (Transform card in enemyArea.transform)
        {
            card.gameObject.SetActive(isVisible);
            card.GetComponent<Drag>().enabled = isVisible;
        }


    }

}
