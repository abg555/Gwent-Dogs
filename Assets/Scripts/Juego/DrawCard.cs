using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{


    public GameObject EnemyArea;
    public GameObject PlayerArea;
    public DeckButton deckButton;
    public GameManager gameManager;



    public void Draw()
    {
        int childCount = EnemyArea.transform.childCount; //guarda la cantidad de hijos de emey area

        if (childCount > 0) //verifica si tien hijos
        {

            int randomIndex = Random.Range(0, childCount);//selecciona una carta random de enemy area
            Transform randomChild = EnemyArea.transform.GetChild(randomIndex);


            gameManager.cementerys.Add(randomChild.gameObject); //la agrega al cementerio
            Destroy(randomChild.gameObject); //ladestruye


            int randomCardIndex = Random.Range(0, deckButton.cards.Count);
            GameObject randomCard = deckButton.gameObjects[randomCardIndex]; //selecciona una carta random de la lista cards

            GameObject newCard = Instantiate(randomCard, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.transform.SetParent(PlayerArea.transform, false); //a instacia en player area
            newCard.SetActive(false);//la hace invisibles
            Debug.Log("holiss");
        }
        else
        {
            Debug.Log("No hay más cartas en EnemyArea para dibujar.");
        }
    }

}







