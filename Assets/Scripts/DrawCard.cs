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
        int childCount = EnemyArea.transform.childCount;

        if (childCount > 0)
        {

            int randomIndex = Random.Range(0, childCount);
            Transform randomChild = EnemyArea.transform.GetChild(randomIndex);


            gameManager.cementerys.Add(randomChild.gameObject);
            Destroy(randomChild.gameObject);


            int randomCardIndex = Random.Range(0, deckButton.cards.Count);
            GameObject randomCard = deckButton.cards[randomCardIndex];

            GameObject newCard = Instantiate(randomCard, new Vector3(0, 0, 0), Quaternion.identity);
            newCard.transform.SetParent(PlayerArea.transform, false);
            newCard.SetActive(false);
            Debug.Log("holiss");
        }
        else
        {
            Debug.Log("No hay más cartas en EnemyArea para dibujar.");
        }
    }

}







