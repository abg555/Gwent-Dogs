
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
// using UnityEngine.UIElements;
using UnityEngine.UI;

public class TurnButton : MonoBehaviour
{
    public GameObject playerArea;
    public GameObject enemyArea;
    public Button yourButton;
    public bool isPlayerEnd = false;
    public bool isEnemyEnd = false;

    public bool isPlayerTurn = true;
    public bool turn = false;
    public int Count = 0;
    public int Count2 = 0;





    // Start is called before the first frame update
    void Start()//se llama a la función ChangeTurn y se añade un listener al botón para que cuando se haga clic en él, se llame a la función TaskOnClick
    {

        ChangeTurn();
        yourButton.onClick.AddListener(TaskOnClick);

    }


    public void ChangeTurn()
    {
        if (turn)
        {
            return;
        }

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
    public void TaskOnClick()//Esta función se llama cuando se hace clic en el botón. Dependiendo de si es el turno del jugador o del enemigo, establece la visibilidad de las áreas correspondientes, cambia el valor de isPlayerTurn, incrementa el contador correspondiente, y establece turn a verdadero
    {

        if (isPlayerTurn)
        {
            Debug.Log("El jugador tocó el botón");

            SetPlayerAreaVisibility(false);
            SetEnemyAreaVisibility(true);
            isPlayerTurn = false;
            Count++;
            turn = true;




        }
        else if (!isPlayerTurn)
        {
            Debug.Log("El enemigo tocó el botón");

            SetPlayerAreaVisibility(true);
            SetEnemyAreaVisibility(false);
            isPlayerTurn = true;
            Count2++;
            turn = true;



        }
        Debug.Log(Count);
        Debug.Log(Count2);

    }

}

